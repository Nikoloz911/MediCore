using Microsoft.AspNetCore.Mvc;

namespace MediCore.Configurations
{
    public class ApiBehaviorConfiguration
    {
        // This disables the automatic 400 Bad Request response in ASP.NET
        public void ConfigureApiBehavior(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
