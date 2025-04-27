using MediCore.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace MediCore.Configurations;
public static class AuthenticationConfiguration
{
    public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
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
                IssuerSigningKey = new SymmetricSecurityKey(        // KEY
                    Encoding.UTF8.GetBytes(configuration["JWT:Key"] ?? "YourSecureJwtKeyHereMakeItLongAndSecure12345")
                ),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("ADMIN"));
            options.AddPolicy("DoctorOnly", policy => policy.RequireRole("DOCTOR"));
            options.AddPolicy("NurseOnly", policy => policy.RequireRole("NURSE"));
        });
    }
}
