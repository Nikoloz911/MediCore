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
namespace MediCore.Services.Imlementations;
public class AuthorizationService : IAuthorization
{
    private readonly DataContext _context;
    private readonly IValidator<AddUser> _userValidator;
    private readonly IMapper _mapper;

    public AuthorizationService(DataContext context, IValidator<AddUser> userValidator, IMapper mapper)
    {
        _context = context;
        _userValidator = userValidator;
        _mapper = mapper;
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

        USER_ROLE role;

        bool isValidRole = Enum.TryParse(requestDto.Role, true, out role) &&
                           Enum.IsDefined(typeof(USER_ROLE), role) &&
                           role != USER_ROLE.ADMIN;

        if (!isValidRole)
        {
            role = USER_ROLE.PATIENT;
        }

        var newUser = _mapper.Map<User>(requestDto);
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(requestDto.Password);
        newUser.Role = role;
        newUser.Status = USER_STATUS.INACTIVE;
        _context.Users.Add(newUser);
        _context.SaveChanges();
        // Send verification email
        // Store Code to expirity
        string verificationCode = SMTP_Registration.GenerateVerificationCode();
        try
        {
            SMTP_Registration.EmailSender(newUser.Email, verificationCode);
            response.Message = "User registered successfully. Verification code sent to email.";
        }
        catch (Exception ex)
        {
            response.Message = "User registered successfully. Verification email failed to send.";
        }

        // Map and return response
        var userDto = _mapper.Map<PublicUserDTO>(newUser);
        response.Status = 200;
        response.Message = "User registered successfully.";
        response.Data = userDto;
        return response;
    }




    public User LogIn(User user)
    {
        throw new NotImplementedException();
    }
    public User RefreshToken(User user)
    {
        throw new NotImplementedException();
    }
}
