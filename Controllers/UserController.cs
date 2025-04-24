using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediCore.Services.Interaces;
using MediCore.Models;
using MediCore.DTOs.UserDTOs;
using AutoMapper;
using System.Collections.Generic;
using MediCore.Core;

namespace MediCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly IMapper _mapper;

        public UserController(IUser userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET ALL USERS (Only Admin can access)
        [HttpGet("users")]
        public ActionResult<UserApiResponse<List<UserGetDTO>>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            if (users == null)
            {
                var response = new UserApiResponse<List<UserGetDTO>>
                {
                    Status = StatusCodes.Status403Forbidden,
                    Data = null,
                    Message = "You are not authorized to access this resource"
                };
                return StatusCode(StatusCodes.Status403Forbidden, response);
            }

            var userDtos = _mapper.Map<List<UserGetDTO>>(users);
            var userResponse = new UserApiResponse<List<UserGetDTO>>
            {
                Status = StatusCodes.Status200OK,
                Data = userDtos,
                Message = "Users retrieved successfully"
            };
            return Ok(userResponse);
        }

        // GET USER BY ID
        [HttpGet("user/{id}")]
        public ActionResult<UserApiResponse<UserGetByIdDTO>> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                var response = new UserApiResponse<UserGetByIdDTO>
                {
                    Status = StatusCodes.Status404NotFound,
                    Data = null,
                    Message = "User not found"
                };
                return NotFound(response);
            }

            var userDto = _mapper.Map<UserGetByIdDTO>(user);
            var userResponse = new UserApiResponse<UserGetByIdDTO>
            {
                Status = StatusCodes.Status200OK,
                Data = userDto,
                Message = "User retrieved successfully"
            };
            return Ok(userResponse);
        }

        // UPDATE USER BY ID
        [HttpPut("users/{id}")]
        public ActionResult<UserApiResponse<UserGetByIdDTO>> UpdateUserById(int id, UserUpdateDTO userUpdateDto)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                var response = new UserApiResponse<UserGetByIdDTO>
                {
                    Status = StatusCodes.Status404NotFound,
                    Data = null,
                    Message = "User not found"
                };
                return NotFound(response);
            }
            _mapper.Map(userUpdateDto, user);
            var updatedUser = _userService.UpdateUserById(id, user);
            var userDto = _mapper.Map<UserGetByIdDTO>(updatedUser);
            var userResponse = new UserApiResponse<UserGetByIdDTO>
            {
                Status = StatusCodes.Status200OK,
                Data = userDto,
                Message = "User updated successfully"
            };
            return Ok(userResponse);
        }

        // DELETE USER BY ID (Admin)
        [HttpDelete("users/{id}")]
        public ActionResult<UserApiResponse<bool>> DeleteUserById(int id)
        {
            var deletedUser = _userService.DeleteUserById(id);
            if (deletedUser == null)
            {
                var response = new UserApiResponse<bool>
                {
                    Status = StatusCodes.Status404NotFound,
                    Data = false,
                    Message = "User not found or you are not authorized to delete this user"
                };
                return NotFound(response);
            }

            var userResponse = new UserApiResponse<bool>
            {
                Status = StatusCodes.Status200OK,
                Data = true,
                Message = "User deleted successfully"
            };
            return Ok(userResponse);
        }
    }
}