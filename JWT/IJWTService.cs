using MediCore.Core;
using MediCore.Models;

namespace MediCore.JWT;
public interface IJWTService
{
    UserToken GetUserToken(User user);
}
