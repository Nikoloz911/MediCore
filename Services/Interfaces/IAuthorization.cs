using MediCore.Core;
using MediCore.DTOs.UserDTOs;
using MediCore.Models;
using MediCore.Request;
namespace MediCore.Services.Interfaces;
public interface IAuthorization
{
    ApiResponse<PublicUserDTO> Register(AddUserDTO request);
    ApiResponse<LogInUserDTO> LogIn(User user);
    ApiResponse<PublicUserDTO> VerifyEmail(string verificationCode);
    ApiResponse<LogInUserDTO> RefreshToken(TokenRefreshRequestDTO request);
    ApiResponse<string> Logout(TokenRefreshRequestDTO request);
}
