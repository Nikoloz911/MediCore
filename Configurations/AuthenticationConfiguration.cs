using MediCore.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MediCore.Configurations
{
    public class AuthenticationConfiguration
    {
        private readonly IConfiguration _configuration;
        public AuthenticationConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureJwtAuthentication(IServiceCollection services)
        {
            services.AddScoped<IJWTService, JWTService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "MediCore",
                    ValidAudience = "application",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["JWT:Key"] ?? "fb2b620a4e15d76fe5e22007a7a5d28945669c7bccdfd94dc8cc618afae38049f063cf1758aeed5b9e4433945b3d210e58e677f60a7c3f68b9b8d3cde286b316f444ae549c73081e49b75de00cc3ac75ab9ee738394b56585df009eb5ea19850af986a817ec3105910f94b7282d003b08043a80bb3aeec7706b4717922b38a0d6937571e0ae9b2ced1bd76e84cd626dd8065acc201f9d8104037e431f836f3a49d498a39c701888caa40819f0a4cade40e0802a35bcbe0aec40a9bc5777ec10dbee139cc78a48f23784ec45bb1e9a7c1f8f0f21bdcd54de1fe91c08c58e8b8db91cd2aaafba53dfe5f663a571401a04bdf3d0e3dbef73ffc4adfcad6c0f0523500199b2f3b2802a519baccd8d435080139bf1823dae89c4d061cd684ab0a46fdb5e400e81f7551a4a60dd25e5fc9a1875ffa0aebff3ef418be1118e328416b15")
                    ),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("ADMIN"));
                options.AddPolicy("DoctorOnly", policy => policy.RequireRole("DOCTOR"));
                options.AddPolicy("NurseOnly", policy => policy.RequireRole("NURSE"));
                // COMBINE POLICIES
                options.AddPolicy("AdminOrDoctor", policy =>
                 policy.RequireRole("ADMIN", "DOCTOR"));
            });
        }
    }
}
