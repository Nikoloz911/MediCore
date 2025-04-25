using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediCore.Services.Interaces;
using MediCore.DTOs.UserDTOs;

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
        public IActionResult GetAllUsers()
        {
            var response = _userService.GetAllUsers();


            return Ok(response);
        }

        [HttpGet("user/{id}")]
        public IActionResult GetUserById(int id)
        {
            var response = _userService.GetUserById(id);
            return Ok(response);
        }

        [HttpPut("users/{id}")]
        public IActionResult UpdateUserById(int id, UserUpdateDTO userUpdateDto)
        {
            var response = _userService.UpdateUserById(id, userUpdateDto);
            return Ok(response);
        }

        [HttpDelete("users/{id}")]
        public IActionResult DeleteUserById(int id)
        {
            var response = _userService.DeleteUserById(id);
            return Ok(response);
        }
    }
}
