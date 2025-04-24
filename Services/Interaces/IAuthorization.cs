using MediCore.Core;
using MediCore.DTOs.UserDTOs;
using MediCore.Models;
using MediCore.Request;
namespace MediCore.Services.Interaces;
public interface IAuthorization
{
    UserApiResponse<PublicUserDTO> Register(AddUserDTO request);
    User LogIn(User user);
    User RefreshToken(User user);
}
