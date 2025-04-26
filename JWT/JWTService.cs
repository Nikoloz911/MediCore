using MediCore.Core;
using MediCore.Enums;
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

        string mappedRole = user.Role switch
        {
            USER_ROLE.ADMIN => "Admin",
            USER_ROLE.DOCTOR => "Doctor",
            USER_ROLE.NURSE => "Nurse",
            USER_ROLE.PATIENT => "Patient",
            _ => throw new ArgumentOutOfRangeException()
        };

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
        new Claim(ClaimTypes.Role, mappedRole),
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
