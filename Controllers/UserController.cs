using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediCore.Services.Interaces;
using MediCore.Models;
using MediCore.DTOs.UserDTOs;
using AutoMapper;
using MediCore.Core;

namespace MediCore.Controllers
{
    [Route("api/")]
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
                    Message = "You are not authorized to access this resource",
                    Data = null,              
                };
                return StatusCode(StatusCodes.Status403Forbidden, response);
            }

            var userDtos = _mapper.Map<List<UserGetDTO>>(users);
            var userResponse = new UserApiResponse<List<UserGetDTO>>
            {
                Status = StatusCodes.Status200OK, 
                Message = "Users retrieved successfully",
                Data = userDtos,
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
                    Message = "User not found",
                    Data = null,
                };
                return NotFound(response);
            }

            var userDto = _mapper.Map<UserGetByIdDTO>(user);
            var userResponse = new UserApiResponse<UserGetByIdDTO>
            {
                Status = StatusCodes.Status200OK,             
                Message = "User retrieved successfully",
                Data = userDto,
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
                    Message = "User not found",
                    Data = null,
                };
                return NotFound(response);
            }
            _mapper.Map(userUpdateDto, user);
            var updatedUser = _userService.UpdateUserById(id, user);
            var userDto = _mapper.Map<UserGetByIdDTO>(updatedUser);
            var userResponse = new UserApiResponse<UserGetByIdDTO>
            {
                Status = StatusCodes.Status200OK,           
                Message = "User updated successfully",
                Data = userDto,
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
                    Message = "User not found or you are not authorized to delete this user",
                    Data = false,
                };
                return NotFound(response);
            }

            var userResponse = new UserApiResponse<bool>
            {
                Status = StatusCodes.Status200OK,        
                Message = "User deleted successfully",
                Data = true,
            };
            return Ok(userResponse);
        }
    }
}