using MediCore.Core;
using MediCore.Enums;
using MediCore.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediCore.JWT
{
    public class JWTService : IJWTService
    {
        private readonly string _jwtKey = "YourSecureJwtKeyHereMakeItLongAndSecure12345";
        private readonly string _jwtIssuer = "MediCore";
        private readonly string _jwtAudience = "application";
        private readonly int _jwtDuration = 300; // in minutes

        public UserToken GetUserToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
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
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                expires: DateTime.Now.AddMinutes(_jwtDuration),
                claims: claims,
                signingCredentials: credentials
            );

            return new UserToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
                ValidateLifetime = false // We want to read claims even if token is expired
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            // VALIDATE TOKEN
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                return principal;
            }
            catch (FormatException ex)
            {
                throw new SecurityTokenInvalidSignatureException("The token format is invalid. Please check the token.", ex);
            }
            catch (SecurityTokenMalformedException ex)
            {
                throw new SecurityTokenMalformedException("The token is malformed. It may not have the correct number of segments (JWT must have three segments).", ex);
            }
            catch (SecurityTokenException ex)
            {
                throw new SecurityTokenInvalidSignatureException("Token validation failed. The token may be expired or improperly formatted.", ex);
            }
        }
    }
}
