using MediCore.Core;
using MediCore.DTOs.UserDTOs;
using MediCore.Models;
using MediCore.Request;
using MediCore.Services.Interaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.Controllers;

[Route("api/[controller]")]
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
    public ActionResult<UserApiResponse<PublicUserDTO>> Register([FromBody] AddUserDTO request)
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
}
