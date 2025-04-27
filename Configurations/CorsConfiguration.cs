using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace MediCore.Configurations
{
    public class CorsConfiguration
    {
        public void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }
    }
}
