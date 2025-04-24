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
    public UserApiResponse<User> Register(AddUserDTO requestDto)
    {
        var response = new UserApiResponse<User>();
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

        USER_ROLE role = USER_ROLE.PATIENT;
        Enum.TryParse(requestDto.Role, true, out role);

        var newUser = _mapper.Map<User>(requestDto);
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(requestDto.Password);
        newUser.Role = role;
        newUser.Status = USER_STATUS.ACTIVE;

        _context.Users.Add(newUser);
        _context.SaveChanges();

        response.Status = 200;
        response.Message = "User registered successfully.";
        response.Data = newUser;
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
