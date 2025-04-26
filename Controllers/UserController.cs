using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediCore.Services.Interaces;
using MediCore.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;

namespace MediCore.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;

        public UserController(IUser userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult GetAllUsers()
        {
            var response = _userService.GetAllUsers();

            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else if (response.Status == StatusCodes.Status403Forbidden)
            {
                return StatusCode(StatusCodes.Status403Forbidden, response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpGet("user/{id}")]
        public IActionResult GetUserById(int id)
        {
            var response = _userService.GetUserById(id);

            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else if (response.Status == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPut("users/{id}")]
        public IActionResult UpdateUserById(int id, UserUpdateDTO userUpdateDto)
        {
            var response = _userService.UpdateUserById(id, userUpdateDto);

            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else if (response.Status == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpDelete("users/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult DeleteUserById(int id)
        {
            var response = _userService.DeleteUserById(id);

            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else if (response.Status == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }
    }
}