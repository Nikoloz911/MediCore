using MediCore.Data;
using MediCore.Models;
using MediCore.DTOs.UserDTOs;
using MediCore.Services.Interaces;
using MediCore.Core;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MediCore.Services.Imlementations
{
    public class UserService : IUser
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UserApiResponse<List<UserGetDTO>> GetAllUsers()
        {
            var users = _context.Users.ToList();

            if (users == null || !users.Any())
            {
                return new UserApiResponse<List<UserGetDTO>>
                {
                    Status = StatusCodes.Status403Forbidden,
                    Message = "No users found or unauthorized access.",
                    Data = null
                };
            }

            var userDtos = _mapper.Map<List<UserGetDTO>>(users);

            return new UserApiResponse<List<UserGetDTO>>
            {
                Status = StatusCodes.Status200OK,
                Message = "Users retrieved successfully",
                Data = userDtos
            };
        }

        public UserApiResponse<UserGetByIdDTO> GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return new UserApiResponse<UserGetByIdDTO>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "User not found",
                    Data = null
                };
            }

            var userDto = _mapper.Map<UserGetByIdDTO>(user);

            return new UserApiResponse<UserGetByIdDTO>
            {
                Status = StatusCodes.Status200OK,
                Message = "User retrieved successfully",
                Data = userDto
            };
        }

        public UserApiResponse<UserGetByIdDTO> UpdateUserById(int id, UserUpdateDTO userUpdateDto)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                return new UserApiResponse<UserGetByIdDTO>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "User not found",
                    Data = null
                };
            }

            _mapper.Map(userUpdateDto, existingUser);
            _context.SaveChanges();

            var updatedDto = _mapper.Map<UserGetByIdDTO>(existingUser);

            return new UserApiResponse<UserGetByIdDTO>
            {
                Status = StatusCodes.Status200OK,
                Message = "User updated successfully",
                Data = updatedDto
            };
        }

        public UserApiResponse<User> DeleteUserById(int id)
        {
            var userToDelete = _context.Users.FirstOrDefault(u => u.Id == id);

            if (userToDelete == null)
            {
                return new UserApiResponse<User>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "User not found",
                    Data = null
                };
            }

            _context.Users.Remove(userToDelete);
            _context.SaveChanges();

            return new UserApiResponse<User>
            {
                Status = StatusCodes.Status200OK,
                Message = "User deleted successfully",
                Data = userToDelete
            };
        }
    
    }
}
