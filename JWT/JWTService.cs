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
        private readonly string _jwtKey = "fb2b620a4e15d76fe5e22007a7a5d28945669c7bccdfd94dc8cc618afae38049f063cf1758aeed5b9e4433945b3d210e58e677f60a7c3f68b9b8d3cde286b316f444ae549c73081e49b75de00cc3ac75ab9ee738394b56585df009eb5ea19850af986a817ec3105910f94b7282d003b08043a80bb3aeec7706b4717922b38a0d6937571e0ae9b2ced1bd76e84cd626dd8065acc201f9d8104037e431f836f3a49d498a39c701888caa40819f0a4cade40e0802a35bcbe0aec40a9bc5777ec10dbee139cc78a48f23784ec45bb1e9a7c1f8f0f21bdcd54de1fe91c08c58e8b8db91cd2aaafba53dfe5f663a571401a04bdf3d0e3dbef73ffc4adfcad6c0f0523500199b2f3b2802a519baccd8d435080139bf1823dae89c4d061cd684ab0a46fdb5e400e81f7551a4a60dd25e5fc9a1875ffa0aebff3ef418be1118e328416b15";
        private readonly string _jwtIssuer = "MediCore";
        private readonly string _jwtAudience = "application";
        private readonly int _jwtDuration = 300; // in minutes

        public UserToken GetUserToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string mappedRole = user.Role switch
            {
                USER_ROLE.ADMIN => "ADMIN",
                USER_ROLE.DOCTOR => "DOCTOR",
                USER_ROLE.NURSE => "NURSE",
                USER_ROLE.PATIENT => "PATIENT",
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
                throw new SecurityTokenMalformedException("The token is malformed. It may not have the correct number of segments.", ex);
            }
            catch (SecurityTokenException ex)
            {
                throw new SecurityTokenInvalidSignatureException("Token validation failed. The token may be expired or improperly formatted.", ex);
            }
        }
    }
}
