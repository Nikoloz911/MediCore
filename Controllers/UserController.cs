using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediCore.Services.Interfaces;
using MediCore.DTOs.UserDTOs;
using MediCore.Core;
using MediCore.Models;
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
        // GET ALL USERS
        [HttpGet("users")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult GetAllUsers()
        {
            var response = _userService.GetAllUsers();
            if (response.Status == StatusCodes.Status200OK)  // OK
            {
                return Ok(response);
            }
            else if (response.Status == StatusCodes.Status404NotFound)  // NOT FOUND
            {
                return NotFound(response);
            }
            else
            {
                return null;
            }
        }
        // GET USER BY ID
        [HttpGet("users/{id}")]
        public IActionResult GetUserById(int id)
        {
            var response = _userService.GetUserById(id);
            if (response.Status == StatusCodes.Status200OK)  // OK
            {
                return Ok(response);
            }
            else if (response.Status == StatusCodes.Status404NotFound)   // NOT FOUND
            {
                return NotFound(response);
            }
            else
            {
                return null;
            }
        }
        // UPDATE USER BY ID
        [HttpPut("users/{id}")]
        public IActionResult UpdateUserById(int id, UserUpdateDTO userUpdateDto)
        {
            var response = _userService.UpdateUserById(id, userUpdateDto);

            if (response.Status == StatusCodes.Status200OK)    // OK
            {
                return Ok(response);
            }
            else if (response.Status == StatusCodes.Status204NoContent)  // NO CONTENT
            {
                return NoContent();
            }
            else if (response.Status == StatusCodes.Status400BadRequest)  // BAD REQUEST
            {
                return BadRequest(response);
            }
            else if (response.Status == StatusCodes.Status404NotFound)    // NOT FOUND
            {
                return NotFound(response);
            }
            else if (response.Status == StatusCodes.Status409Conflict)   // CONFLICT
            {
                return Conflict(response);
            }
            else
            {
                return null;
            }
        }
        // DELETE USER BY ID
        [HttpDelete("users/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult DeleteUserById(int id)
        {
            var response = _userService.DeleteUserById(id);

            if (response.Status == StatusCodes.Status200OK)   // OK
            {
                return Ok(response);
            }    
            else if (response.Status == StatusCodes.Status404NotFound)     // NOT FOUND
            {
                return NotFound(response);
            }
            else
            {
                return null;
            }
        }
    }
}