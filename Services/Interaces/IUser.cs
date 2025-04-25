using MediCore.Core;
using MediCore.DTOs.UserDTOs;
using MediCore.Models;
namespace MediCore.Services.Interaces;
public interface IUser
{
    UserApiResponse<List<UserGetDTO>> GetAllUsers();    // ADMIN
    UserApiResponse<UserGetByIdDTO> GetUserById(int id);
    UserApiResponse<UserGetByIdDTO> UpdateUserById(int id, UserUpdateDTO userUpdateDto);
    UserApiResponse<User> DeleteUserById(int id);   // ADMIN
}
