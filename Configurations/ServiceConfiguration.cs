using FluentValidation.AspNetCore;
using MediCore.Data;
using MediCore.Services.Implementations;
using MediCore.Services.Interfaces;
using MediCore.Validators;

namespace MediCore.Configurations
{
    public class ServiceConfiguration
    {
        public void ConfigureApplicationServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>();
            services.AddScoped<IAuthorization, AuthorizationService>();
            services.AddScoped<IUser, UserService>();
            services.AddScoped<IDoctor, DoctorService>();
            services.AddAutoMapper(typeof(Program));
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());
        }
    }
}
