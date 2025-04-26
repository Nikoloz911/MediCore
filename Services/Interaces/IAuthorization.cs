using MediCore.Core;
using MediCore.DTOs.UserDTOs;
using MediCore.Models;
using MediCore.Request;
namespace MediCore.Services.Interaces;
public interface IAuthorization
{
    UserApiResponse<PublicUserDTO> Register(AddUserDTO request);
    UserApiResponse<LogInUserDTO> LogIn(User user);
    UserApiResponse<PublicUserDTO> VerifyEmail(string verificationCode);
    UserApiResponse<LogInUserDTO> RefreshToken(TokenRefreshRequestDTO request);
    UserApiResponse<string> Logout(TokenRefreshRequestDTO request);
}
