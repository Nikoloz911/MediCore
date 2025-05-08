using MediCore.Services.Interfaces;
using MediCore.Data;
using MediCore.Models;
using MediCore.Core;
using MediCore.Request;
using MediCore.Validators;
using BCrypt.Net;
using MediCore.Enums;
using FluentValidation;
using MediCore.DTOs.UserDTOs;
using AutoMapper;
using MediCore.SMTP;
using MediCore.JWT;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
namespace MediCore.Services.Implementations;
public class AuthorizationService : IAuthorization
{
    private readonly DataContext _context;
    private readonly IValidator<AddUser> _userValidator;
    private readonly IMapper _mapper;
    private readonly IJWTService _jwtService;

    public AuthorizationService(DataContext context, IValidator<AddUser> userValidator, IMapper mapper, IJWTService jwtService)
    {
        _context = context;
        _userValidator = userValidator;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    // REGISTER A NEW USER
    public ApiResponse<PublicUserDTO> Register(AddUserDTO requestDto)
    {
        var response = new ApiResponse<PublicUserDTO>();
        var request = _mapper.Map<AddUser>(requestDto);

        var validationResult = _userValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            response.Status = 400;
            response.Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            response.Data = null!;
            return response;
        }
        if (_context.Users.Any(u => u.Email == requestDto.Email))
        {
            response.Status = 409;
            response.Message = "User with this email already exists.";
            response.Data = null!;
            return response;
        }
        // Validate role
        USER_ROLE role;
        bool isValidRole = Enum.TryParse(requestDto.Role, true, out role) &&
                           Enum.IsDefined(typeof(USER_ROLE), role);
        if (!isValidRole)
        {
            response.Status = 400;
            response.Message = $"Invalid user role: {requestDto.Role}";
            return response;
        }
        // Map AddUserDTO to User
        var newUser = _mapper.Map<User>(requestDto);
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(requestDto.Password);
        newUser.Role = role;
        newUser.Status = USER_STATUS.UNVERIFIED;
        //  Send the email and generated verification code with expiry time
        string verificationCode = SMTP_Registration.GenerateVerificationCode();
        newUser.VerificationCode = verificationCode;
        newUser.VerificationCodeExpiry = DateTime.UtcNow.AddMinutes(5);
        try
        {
            SMTP_Registration.EmailSender(newUser.Email, newUser.FirstName, newUser.LastName, verificationCode);
        }
        catch (Exception ex)
        {
            response.Status = 500;
            return response;
        }
        // Save the new user in the database
        _context.Users.Add(newUser);
        _context.SaveChanges();
        WriteUserToExcel(newUser);
        // Map User to PublicUserDTO for response
        var userDto = _mapper.Map<PublicUserDTO>(newUser);
        response.Status = 200;
        response.Data = userDto;
        response.Message = "User registered successfully. Please check your email for verification code.";
        return response;
    }

    // SAVE USERS IN EXCEL
    public void WriteUserToExcel(User user)
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(baseDirectory, "Users.xlsx");
        var fileExists = File.Exists(filePath);

        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            ExcelWorksheet worksheet;

            if (!fileExists)
            {
                worksheet = package.Workbook.Worksheets.Add("RegisteredUsers");
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "FirstName";
                worksheet.Cells[1, 3].Value = "LastName";
                worksheet.Cells[1, 4].Value = "Email";
                worksheet.Cells[1, 5].Value = "Role";
            }
            else
            {
                worksheet = package.Workbook.Worksheets["RegisteredUsers"] ??
                            package.Workbook.Worksheets.Add("RegisteredUsers");
                if (worksheet.Dimension == null)
                {
                    worksheet.Cells[1, 1].Value = "Id";
                    worksheet.Cells[1, 2].Value = "FirstName";
                    worksheet.Cells[1, 3].Value = "LastName";
                    worksheet.Cells[1, 4].Value = "Email";
                    worksheet.Cells[1, 5].Value = "Role";
                }
            }

            int newRow = (worksheet.Dimension?.End.Row ?? 1) + 1;
            worksheet.Cells[newRow, 1].Value = user.Id;
            worksheet.Cells[newRow, 2].Value = user.FirstName;
            worksheet.Cells[newRow, 3].Value = user.LastName;
            worksheet.Cells[newRow, 4].Value = user.Email;
            worksheet.Cells[newRow, 5].Value = user.Role.ToString();
            package.Save();
        }
    }

    // VERIFY EMAIL
    public ApiResponse<PublicUserDTO> VerifyEmail(string verificationCode)
    {
        var response = new ApiResponse<PublicUserDTO>();
        var user = _context.Users.FirstOrDefault(u =>
            u.VerificationCode == verificationCode &&
            u.VerificationCodeExpiry > DateTime.UtcNow);
        if (user == null)
        {
            response.Status = 404;
            response.Message = "Invalid or expired verification code";
            response.Data = null;
            return response;
        }
        user.Status = USER_STATUS.VERIFIED;
        user.VerificationCode = null;
        user.VerificationCodeExpiry = DateTime.MinValue;
        _context.Users.Update(user);
        _context.SaveChanges();
        var publicUser = _mapper.Map<PublicUserDTO>(user);
        response.Status = 200;
        response.Message = "Email verification successful";
        response.Data = publicUser;
        return response;
    }
    // LOGIN
    public ApiResponse<LogInUserDTO> LogIn(User user)
    {
        var foundUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
        if (foundUser == null)
        {
            return new ApiResponse<LogInUserDTO>
            {
                Status = 404,
                Message = "User not found",
                Data = null
            };
        }

        if (foundUser.Status == USER_STATUS.UNVERIFIED)
        {
            return new ApiResponse<LogInUserDTO>
            {
                Status = 403,
                Message = "User is not Verified",
                Data = null
            };
        }

        if (!BCrypt.Net.BCrypt.Verify(user.Password, foundUser.Password))
        {
            return new ApiResponse<LogInUserDTO>
            {
                Status = 401,
                Message = "Invalid password",
                Data = null
            };
        }

        var jwtToken = _jwtService.GetUserToken(foundUser);
        foundUser.Status = USER_STATUS.ACTIVE;
        _context.Users.Update(foundUser);
        _context.SaveChanges();
        LogLoginActivity(foundUser);

        var loginUser = new LogInUserDTO
        {
            Email = foundUser.Email,
            Role = foundUser.Role.ToString(),
            Token = jwtToken.Token,
            Status = foundUser.Status.ToString(),
            Password = foundUser.Password
        };

        return new ApiResponse<LogInUserDTO>
        {
            Status = 200,
            Message = "Login successful",
            Data = loginUser
        };
    }
    // LOG THE LOG IN
    private void LogLoginActivity(User user)
    {
        string logFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Security.txt"
        );

        if (!File.Exists(logFilePath))
        {
            File.Create(logFilePath).Close();
        }

        string logEntry =
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Login - User ID {user.Id}, Email: {user.Email}, Role: {user.Role}\n";

        File.AppendAllText(logFilePath, logEntry);
    }

    // LOGOUT
    public ApiResponse<string> Logout(TokenRefreshRequestDTO request)
    {
        if (string.IsNullOrEmpty(request.Token))
        {
            return new ApiResponse<string>
            {
                Status = 400,
                Message = "Token is required",
                Data = null
            };
        }
        ClaimsPrincipal principal;
        try
        {
            principal = _jwtService.GetPrincipalFromExpiredToken(request.Token);
        }
        catch (SecurityTokenMalformedException ex)
        {
            return new ApiResponse<string>
            {
                Status = 401,
                Message = ex.Message, 
                Data = null
            };
        }
        catch (SecurityTokenInvalidSignatureException ex)
        {
            return new ApiResponse<string>
            {
                Status = 401,
                Message = ex.Message, 
                Data = null
            };
        }
        if (principal == null)
        {
            return new ApiResponse<string>
            {
                Status = 401,
                Message = "Invalid token",
                Data = null
            };
        }
        var email = principal.FindFirstValue(ClaimTypes.Email);
        var foundUser = _context.Users.FirstOrDefault(u => u.Email == email);
        if (foundUser != null)
        {
            foundUser.Status = USER_STATUS.INACTIVE;
            _context.Users.Update(foundUser);
            _context.SaveChanges();
        }
        return new ApiResponse<string>
        {
            Status = 200,
            Message = "Logged out successfully",
            Data = "Logout successful"
        };
    }
    // REFRESH TOKEN
    public ApiResponse<LogInUserDTO> RefreshToken(TokenRefreshRequestDTO request)
    {
        if (string.IsNullOrEmpty(request.Token))
        {
            return new ApiResponse<LogInUserDTO>
            {
                Status = 400,
                Message = "Token is required",
                Data = null
            };
        }
        ClaimsPrincipal principal;
        try
        {
            principal = _jwtService.GetPrincipalFromExpiredToken(request.Token);
        }
        catch (SecurityTokenMalformedException ex)
        {
            return new ApiResponse<LogInUserDTO>
            {
                Status = 401,
                Message = ex.Message, 
                Data = null
            };
        }
        catch (SecurityTokenInvalidSignatureException ex)
        {
            return new ApiResponse<LogInUserDTO>
            {
                Status = 401,
                Message = ex.Message, 
                Data = null
            };
        }
        if (principal == null)
        {
            return new ApiResponse<LogInUserDTO>
            {
                Status = 401,
                Message = "Invalid token",
                Data = null
            };
        }
        var email = principal.FindFirstValue(ClaimTypes.Email);
        var foundUser = _context.Users.FirstOrDefault(u => u.Email == email);
        if (foundUser == null)
        {
            return new ApiResponse<LogInUserDTO>
            {
                Status = 400,
                Message = "User not found",
                Data = null
            };
        }
        var newJwtToken = _jwtService.GetUserToken(foundUser);
        var loginUser = new LogInUserDTO
        {
            Email = foundUser.Email,
            Role = foundUser.Role.ToString(),
            Token = newJwtToken.Token,
            Status = foundUser.Status.ToString(),
            Password = foundUser.Password
        };
        return new ApiResponse<LogInUserDTO>
        {
            Status = 200,
            Message = "Token refreshed successfully",
            Data = loginUser
        };
    }
}
