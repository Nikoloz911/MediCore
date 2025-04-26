using MediCore.Data;
using MediCore.Models;
using MediCore.DTOs.UserDTOs;
using MediCore.Services.Interaces;
using MediCore.Core;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text.RegularExpressions;
using BCrypt.Net;

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
            // Find the existing user
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

            // Validate inputs
            var validationErrors = ValidateUserUpdate(userUpdateDto, existingUser);
            if (validationErrors.Count > 0)
            {
                return new UserApiResponse<UserGetByIdDTO>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Validation failed: " + string.Join(", ", validationErrors),
                    Data = null
                };
            }

            if (!string.IsNullOrWhiteSpace(userUpdateDto.Email) &&
                userUpdateDto.Email != existingUser.Email &&
                _context.Users.Any(u => u.Email == userUpdateDto.Email))
            {
                return new UserApiResponse<UserGetByIdDTO>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Email is already in use",
                    Data = null
                };
            }

            bool hasChanges = false;
            if (!string.IsNullOrWhiteSpace(userUpdateDto.FirstName) && userUpdateDto.FirstName != existingUser.FirstName)
            {
                existingUser.FirstName = userUpdateDto.FirstName;
                hasChanges = true;
            }

            if (!string.IsNullOrWhiteSpace(userUpdateDto.LastName) && userUpdateDto.LastName != existingUser.LastName)
            {
                existingUser.LastName = userUpdateDto.LastName;
                hasChanges = true;
            }

            if (!string.IsNullOrWhiteSpace(userUpdateDto.Email) && userUpdateDto.Email != existingUser.Email)
            {
                existingUser.Email = userUpdateDto.Email;
                hasChanges = true;
            }

            if (!string.IsNullOrWhiteSpace(userUpdateDto.Password))
            {
                bool isSamePassword = false;
                try
                {
                    isSamePassword = BCrypt.Net.BCrypt.Verify(userUpdateDto.Password, existingUser.Password);
                }
                catch
                {
                    isSamePassword = false;
                }

                if (!isSamePassword)
                {
                    existingUser.Password = BCrypt.Net.BCrypt.HashPassword(userUpdateDto.Password);
                    hasChanges = true;
                }
            }
            if (hasChanges)
            {
                _context.SaveChanges();
                return new UserApiResponse<UserGetByIdDTO>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "User updated successfully",
                    Data = _mapper.Map<UserGetByIdDTO>(existingUser)
                };
            }
            else
            {
                return new UserApiResponse<UserGetByIdDTO>
                {
                    Status = StatusCodes.Status204NoContent,
                    Message = "No changes were made - all values matched existing data",
                    Data = _mapper.Map<UserGetByIdDTO>(existingUser)
                };
            }
        }

        private List<string> ValidateUserUpdate(UserUpdateDTO userDto, User existingUser)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(userDto.FirstName) &&
                string.IsNullOrWhiteSpace(userDto.LastName) &&
                string.IsNullOrWhiteSpace(userDto.Email) &&
                string.IsNullOrWhiteSpace(userDto.Password))
            {
                errors.Add("No fields provided for update");
                return errors;
            }
            if (!string.IsNullOrWhiteSpace(userDto.FirstName))
            {
                if (string.IsNullOrEmpty(userDto.FirstName))
                {
                    errors.Add("First Name is required.");
                }
                else if (userDto.FirstName.Length < 1 || userDto.FirstName.Length > 40)
                {
                    errors.Add("First Name must be between 1 and 40 characters.");
                }
            }

            if (!string.IsNullOrWhiteSpace(userDto.LastName))
            {
                if (string.IsNullOrEmpty(userDto.LastName))
                {
                    errors.Add("Last Name is required.");
                }
                else if (userDto.LastName.Length < 1 || userDto.LastName.Length > 60)
                {
                    errors.Add("Last Name must be between 1 and 60 characters.");
                }
            }

            if (!string.IsNullOrWhiteSpace(userDto.Email))
            {
                if (string.IsNullOrEmpty(userDto.Email))
                {
                    errors.Add("Email is required.");
                }
                else
                {
                    var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                    if (!Regex.IsMatch(userDto.Email, emailPattern))
                    {
                        errors.Add("Email is not valid.");
                    }

                    if (!userDto.Email.EndsWith("@gmail.com"))
                    {
                        errors.Add("Email must end with '@gmail.com'.");
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                if (string.IsNullOrEmpty(userDto.Password))
                {
                    errors.Add("Password is required.");
                }
                else
                {
                    if (userDto.Password.Length < 4 || userDto.Password.Length > 40)
                    {
                        errors.Add("Password must be between 4 and 40 characters.");
                    }

                    if (!Regex.IsMatch(userDto.Password, @"[A-Z]"))
                    {
                        errors.Add("Password must contain at least one uppercase letter.");
                    }

                    if (!Regex.IsMatch(userDto.Password, @"[0-9]"))
                    {
                        errors.Add("Password must contain at least one number.");
                    }
                }
            }

            return errors;
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
