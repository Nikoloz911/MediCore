using MediCore.Core;
using MediCore.DTOs.UserDTOs;
using MediCore.Models;
using MediCore.Request;
using MediCore.Services.Interaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthorization _authorizationService;
    public AuthenticationController(IAuthorization authorizationService)
    {
        _authorizationService = authorizationService;
    }
    // REGISTER A NEW USER
    [HttpPost("register")]
    public ActionResult<ApiResponse<PublicUserDTO>> Register([FromBody] AddUserDTO request)
    {
        var response = _authorizationService.Register(request);
        if (response.Status == 200)
        {
            return Ok(response);
        }
        else if (response.Status == 400)
        {
            return BadRequest(response);
        }
        else if (response.Status == 409)
        {
            return Conflict(response);
        }
        return StatusCode(500, "Internal Server Error"); 
    }
    // VERIFY EMAIL
    [HttpPost("verify-email")]
    public ActionResult<ApiResponse<PublicUserDTO>> VerifyEmail([FromBody] EmailVerificationDTO verificationDto)
    {
        var response = _authorizationService.VerifyEmail(verificationDto.VerificationCode);

        if (response.Status == 200)
        {
            return Ok(response);
        }
        else if (response.Status == 404)
        {
            return NotFound(response);
        }
        else
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
    // LOGIN
    [HttpPost("login")]
    public ActionResult<ApiResponse<LogInUserDTO>> Login([FromBody] LoginRequestDTO loginDto)
    {
        var user = new User
        {
            Email = loginDto.Email,
            Password = loginDto.Password
        };
        var response = _authorizationService.LogIn(user);
        if (response.Status == 200)
        {
            return Ok(response);
        }
        else if (response.Status == 400)
        {
            return BadRequest(response);
        }
        else if (response.Status == 401)
        {
            return Unauthorized(response);
        }
        else if (response.Status == 403)
        {
            return StatusCode(403, new ApiResponse<LogInUserDTO>
            {
                Status = 403,
                Message = "User is not active",
                Data = null 
            });
        }
        else
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    // LOGOUT
    [HttpPost("logout")]
    public ActionResult<ApiResponse<string>> Logout([FromBody] TokenRefreshRequestDTO request)
    {
        var response = _authorizationService.Logout(request);
        if (response.Status == 200)
        {
            return Ok(response);
        }
        else if (response.Status == 400)
        {
            return BadRequest(response);
        }
        else if (response.Status == 401)
        {
            return Unauthorized(response);
        }
        return StatusCode(500, "Internal Server Error");
    }

    // REFRESH TOKEN
    [HttpPost("refresh-token")]
    public ActionResult<ApiResponse<LogInUserDTO>> RefreshToken([FromBody] TokenRefreshRequestDTO request)
    {
        var response = _authorizationService.RefreshToken(request);
        if (response.Status == 200)
        {
            return Ok(response);
        }
        else if (response.Status == 400)
        {
            return BadRequest(response);
        }
        else if (response.Status == 401)
        {
            return Unauthorized(response);
        }
        return StatusCode(500, "Internal Server Error");
    }
}
