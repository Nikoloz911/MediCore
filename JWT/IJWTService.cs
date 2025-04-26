using MediCore.Core;
using MediCore.Models;
using System.Security.Claims;

namespace MediCore.JWT;
public interface IJWTService
{
    UserToken GetUserToken(User user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
