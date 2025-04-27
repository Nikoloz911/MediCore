using Microsoft.AspNetCore.Mvc;

namespace MediCore.Configurations
{
    public class ApiBehaviorConfiguration
    {
        public void ConfigureApiBehavior(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
