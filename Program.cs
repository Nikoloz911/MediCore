using FluentValidation.AspNetCore;
using MediCore.Data;
using MediCore.Validators;
using MediCore.Services.Imlementations;
using MediCore.Services.Interaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register services
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());


builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IAuthorization, AuthorizationService>();
builder.Services.AddAutoMapper(typeof(Program));

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
