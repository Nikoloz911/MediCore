using MediCore.Core;
using MediCore.DTOs.UserDTOs;
using MediCore.Models;
namespace MediCore.Services.Interfaces;
public interface IUser
{
    ApiResponse<List<UserGetDTO>> GetAllUsers();    // ADMIN
    ApiResponse<UserGetByIdDTO> GetUserById(int id);
    ApiResponse<UserGetByIdDTO> UpdateUserById(int id, UserUpdateDTO userUpdateDto);
    ApiResponse<User> DeleteUserById(int id);   // ADMIN
}
