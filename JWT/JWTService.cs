using MediCore.Core;
using MediCore.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediCore.JWT;
public class JWTService : IJWTService
{
    public UserToken GetUserToken(User user)
    {
        var JwtKey = "YourSecureJwtKeyHereMakeItLongAndSecure12345";
        var JwtIssuer = "MediCore";
        var JwtAudience = "application";
        var JwtDuration = 300; // in minutes

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("role", user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: JwtIssuer,
            audience: JwtAudience,
            expires: DateTime.Now.AddMinutes(JwtDuration),
            claims: claims,
            signingCredentials: credentials
        );

        return new UserToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        };
    }
}
