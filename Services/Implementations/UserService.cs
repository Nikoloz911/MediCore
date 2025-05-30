﻿using System.Text.RegularExpressions;
using AutoMapper;
using BCrypt.Net;
using MediCore.Core;
using MediCore.Data;
using MediCore.DTOs.UserDTOs;
using MediCore.Models;
using MediCore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Services.Implementations
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

        // GET ALL USERS    // GET ALL USERS    // GET ALL USERS    // GET ALL USERS
        public ApiResponse<List<UserGetDTO>> GetAllUsers()
        {
            var users = _context.Users.ToList();
            if (users == null)
            {
                return new ApiResponse<List<UserGetDTO>>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "No users found.",
                    Data = null,
                };
            }
            var userDtos = _mapper.Map<List<UserGetDTO>>(users);
            return new ApiResponse<List<UserGetDTO>>
            {
                Status = StatusCodes.Status200OK,
                Message = "Users retrieved successfully",
                Data = userDtos,
            };
        }

        // GET USER BY ID     // GET USER BY ID     // GET USER BY ID     // GET USER BY ID  
        public ApiResponse<UserGetByIdDTO> GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return new ApiResponse<UserGetByIdDTO>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "User not found",
                    Data = null,
                };
            }
            var userDto = _mapper.Map<UserGetByIdDTO>(user);
            return new ApiResponse<UserGetByIdDTO>
            {
                Status = StatusCodes.Status200OK,
                Message = "User retrieved successfully",
                Data = userDto,
            };
        }

        // UPDATE USER BY ID   // UPDATE USER BY ID   // UPDATE USER BY ID
        public ApiResponse<UserGetByIdDTO> UpdateUserById(int id, UserUpdateDTO userUpdateDto)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return new ApiResponse<UserGetByIdDTO>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "User not found",
                    Data = null,
                };
            }

            var validationErrors = ValidateUserUpdate(userUpdateDto, existingUser);
            if (validationErrors.Count > 0)
            {
                return new ApiResponse<UserGetByIdDTO>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Validation failed: " + string.Join(", ", validationErrors),
                    Data = null,
                };
            }

            if (
                !string.IsNullOrWhiteSpace(userUpdateDto.Email)
                && userUpdateDto.Email != existingUser.Email
                && _context.Users.Any(u => u.Email == userUpdateDto.Email)
            )
            {
                return new ApiResponse<UserGetByIdDTO>
                {
                    Status = StatusCodes.Status409Conflict,
                    Message = "Email is already in use",
                    Data = null,
                };
            }

            bool hasChanges = false;
            List<string> changeLog = new();

            if (
                !string.IsNullOrWhiteSpace(userUpdateDto.FirstName)
                && userUpdateDto.FirstName != existingUser.FirstName
            )
            {
                changeLog.Add(
                    $"FirstName: '{existingUser.FirstName}' -> '{userUpdateDto.FirstName}'"
                );
                existingUser.FirstName = userUpdateDto.FirstName;
                hasChanges = true;
            }

            if (
                !string.IsNullOrWhiteSpace(userUpdateDto.LastName)
                && userUpdateDto.LastName != existingUser.LastName
            )
            {
                changeLog.Add($"LastName: '{existingUser.LastName}' -> '{userUpdateDto.LastName}'");
                existingUser.LastName = userUpdateDto.LastName;
                hasChanges = true;
            }

            if (
                !string.IsNullOrWhiteSpace(userUpdateDto.Email)
                && userUpdateDto.Email != existingUser.Email
            )
            {
                changeLog.Add($"Email: '{existingUser.Email}' -> '{userUpdateDto.Email}'");
                existingUser.Email = userUpdateDto.Email;
                hasChanges = true;
            }

            if (!string.IsNullOrWhiteSpace(userUpdateDto.Password))
            {
                bool isSamePassword = false;
                try
                {
                    isSamePassword = BCrypt.Net.BCrypt.Verify(
                        userUpdateDto.Password,
                        existingUser.Password
                    );
                }
                catch
                {
                    isSamePassword = false;
                }

                if (!isSamePassword)
                {
                    changeLog.Add("Password: changed");
                    existingUser.Password = BCrypt.Net.BCrypt.HashPassword(userUpdateDto.Password);
                    hasChanges = true;
                }
            }

            if (hasChanges)
            {
                _context.SaveChanges();
                LogChanges(id, changeLog);
                return new ApiResponse<UserGetByIdDTO>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "User updated successfully",
                    Data = _mapper.Map<UserGetByIdDTO>(existingUser),
                };
            }
            else
            {
                return new ApiResponse<UserGetByIdDTO>
                {
                    Status = StatusCodes.Status204NoContent,
                    Message = "No changes were made. values matched existing data",
                    Data = _mapper.Map<UserGetByIdDTO>(existingUser),
                };
            }
        }

        // LOG CHANGES    // LOG CHANGES    // LOG CHANGES
        private void LogChanges(int userId, List<string> changes)
        {
            string logFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "UsersUpdate.txt"
            );

            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }

            string logEntry =
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] User ID {userId} updated:\n - {string.Join("\n - ", changes)}\n";
            File.AppendAllText(logFilePath, logEntry);
        }

        // Validate user update    // Validate user update    // Validate user update
        private List<string> ValidateUserUpdate(UserUpdateDTO userDto, User existingUser)
        {
            var errors = new List<string>();
            if (
                string.IsNullOrWhiteSpace(userDto.FirstName)
                && string.IsNullOrWhiteSpace(userDto.LastName)
                && string.IsNullOrWhiteSpace(userDto.Email)
                && string.IsNullOrWhiteSpace(userDto.Password)
            )
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
                    if (!userDto.Email.EndsWith(".com"))
                    {
                        errors.Add("Email must end with '.com'");
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

        // DELETE USER BY ID   // DELETE USER BY ID   // DELETE USER BY ID
        public ApiResponse<User> DeleteUserById(int id)
        {
            var userToDelete = _context
                .Users.Include(u => u.Patient)
                .Include(u => u.Doctor)
                .FirstOrDefault(u => u.Id == id);

            if (userToDelete == null)
            {
                return new ApiResponse<User>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "User not found",
                    Data = null,
                };
            }
            LogDeletion(userToDelete);

            if (userToDelete.Patient != null)
            {
                _context.Patients.Remove(userToDelete.Patient);
            }
            if (userToDelete.Doctor != null)
            {
                _context.Doctors.Remove(userToDelete.Doctor);
            }

            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
            return new ApiResponse<User>
            {
                Status = StatusCodes.Status200OK,
                Message = "User deleted successfully",
                Data = userToDelete,
            };
        }
        // LOG DELETION    // LOG DELETION    // LOG DELETION
        private void LogDeletion(User user)
        {
            string logFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "UsersDeleted.txt"
            );

            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }

            string logEntry =
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Deleted User ID {user.Id}:\n" +
                $" - FirstName: {user.FirstName}\n" +
                $" - LastName: {user.LastName}\n" +
                $" - Email: {user.Email}\n" +
                $" - HasPatientRecord: {(user.Patient != null)}\n" +
                $" - HasDoctorRecord: {(user.Doctor != null)}\n" +
                "----------------------------\n";

            File.AppendAllText(logFilePath, logEntry);
        }
    }
}
