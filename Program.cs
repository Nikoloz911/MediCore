using FluentValidation.AspNetCore;
using MediCore.Data;
using MediCore.Validators;
using MediCore.Services.Implementations;
using MediCore.Services.Interfaces;
using MediCore.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MediCore.Core;
using Microsoft.AspNetCore.Diagnostics;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// REGISTER SERVICES  /// REGISTER SERVICES  /// REGISTER SERVICES  /// REGISTER SERVICES  /// REGISTER SERVICES

builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IAuthorization, AuthorizationService>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IDoctor, DoctorService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());

// JWT Token configuration
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddAuthentication(options =>
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration["JWT:Key"] ?? "YourSecureJwtKeyHereMakeItLongAndSecure12345")),
            ClockSkew = TimeSpan.Zero,
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("ADMIN"));
    options.AddPolicy("DoctorOnly", policy => policy.RequireRole("DOCTOR"));
    options.AddPolicy("NurseOnly", policy => policy.RequireRole("NURSE"));
});

// DISABLE AUTOMATIC 400 RESPONSES
// FOR DoctorController HTTPPUT
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
/// REGISTER SERVICES  /// REGISTER SERVICES  /// REGISTER SERVICES  /// REGISTER SERVICES  /// REGISTER SERVICES


var app = builder.Build();
app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionHandlerFeature.Error;
        if (exception is JsonException || exception.InnerException is JsonException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var response = new ApiResponse<object>
            {
                Status = StatusCodes.Status400BadRequest,
                Message = "Invalid JSON format in request body",
                Data = null
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var response = new ApiResponse<object>
            {
                Status = StatusCodes.Status500InternalServerError,
                Message = "An unexpected error occurred",
                Data = null
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    });
});
/// TO ADD DATA IN DATABASE
/// 
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var context = services.GetRequiredService<DataContext>();
//    AddDoctorsData.SeedDoctorsData(context);
//}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();