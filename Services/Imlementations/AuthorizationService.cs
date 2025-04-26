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

        //  Send the email and generated verification code
        string verificationCode = SMTP_Registration.GenerateVerificationCode();
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
        newUser.VerificationCode = verificationCode;
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

        if (foundUser.Status != USER_STATUS.ACTIVE)
        {
            foundUser.Status = USER_STATUS.ACTIVE;
            _context.Users.Update(foundUser);
            _context.SaveChanges();
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




    public User RefreshToken(User user)
    {
        throw new NotImplementedException();
    }
}
