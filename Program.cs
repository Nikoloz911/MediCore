using FluentValidation.AspNetCore;
using MediCore.Configurations; 
using MediCore.Data;          
using MediCore.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register your services from Configurations folder
builder.Services.ConfigureApplicationServices();       
builder.Services.ConfigureJwtAuthentication(builder.Configuration); 
builder.Services.ConfigureCors();                       
builder.Services.ConfigureApiBehavior();               
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>()); 

var app = builder.Build();

app.UseCustomExceptionHandler();  
// app.InitializeData();  // Seed data

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();                    // Enable CORS
app.UseAuthentication();          // Enable authentication (JWT)
app.UseAuthorization();           // Enable authorization
app.UseHttpsRedirection();
app.MapControllers();           
app.Run();                       