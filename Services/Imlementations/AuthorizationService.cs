using MediCore.Services.Interaces;
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
namespace MediCore.Services.Imlementations;
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
    public UserApiResponse<PublicUserDTO> Register(AddUserDTO requestDto)
    {
        var response = new UserApiResponse<PublicUserDTO>();
        var request = _mapper.Map<AddUser>(requestDto);

        var validationResult = _userValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            response.Status = 400;
            response.Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            response.Data = null!;
            return response;
        }   
        //if (_context.Users.Any(u => u.Email == requestDto.Email))
        //{
        //    response.Status = 409;
        //    response.Message = "User with this email already exists.";
        //    response.Data = null!;
        //    return response;
        //}

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
        newUser.Status = USER_STATUS.INACTIVE;
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
        // Generate JWT token
        // Map User to PublicUserDTO for response
        var userDto = _mapper.Map<PublicUserDTO>(newUser);
        response.Status = 200;
        response.Data = userDto;
        return response;
    }

    public UserApiResponse<LogInUserDTO> LogIn(User user)
    {
        var foundUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
        if (foundUser == null)
        {
            return new UserApiResponse<LogInUserDTO>
            {
                Status = 400,
                Message = "User not found",
                Data = null
            };
        }
        if (!BCrypt.Net.BCrypt.Verify(user.Password, foundUser.Password))
        {
            return new UserApiResponse<LogInUserDTO>
            {
                Status = 401,
                Message = "Invalid password",
                Data = null
            };
        }
        var jwtToken = _jwtService.GetUserToken(foundUser);
        var loginUser = new LogInUserDTO
        {
            Email = foundUser.Email,
            Role = foundUser.Role.ToString(),
            Token = jwtToken.Token,
            Status = foundUser.Status.ToString(),
            Password = foundUser.Password
        };
        return new UserApiResponse<LogInUserDTO>
        {
            Status = 200,
            Message = "Login successful",
            Data = loginUser
        };
    }

    public UserApiResponse<PublicUserDTO> VerifyEmail(string verificationCode)
    {
        var response = new UserApiResponse<PublicUserDTO>();
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
        user.Status = USER_STATUS.ACTIVE;
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
    // Cleanup expired verification codes
    public void CleanupExpiredVerificationCodes()
    {
        var expiredUsers = _context.Users
            .Where(u => u.Status == USER_STATUS.INACTIVE &&
                       u.VerificationCodeExpiry < DateTime.UtcNow &&
                       u.VerificationCode != null)
            .ToList();
        foreach (var user in expiredUsers)
        {
            user.VerificationCode = null;
            user.VerificationCodeExpiry = DateTime.MinValue;
        }
        _context.SaveChanges();
    }

    public User RefreshToken(User user)
    {
        throw new NotImplementedException();
    }
}
