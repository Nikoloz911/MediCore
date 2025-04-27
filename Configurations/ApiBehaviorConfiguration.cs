using Microsoft.AspNetCore.Mvc;

namespace MediCore.Configurations
{
    public static class ApiBehaviorConfiguration
    {
        public static void ConfigureApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
