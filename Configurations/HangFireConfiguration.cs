using Hangfire;
using Hangfire.SqlServer;
using MediCore.Services.Implementations;
using MediCore.SMTP;

namespace MediCore.Configurations
{
    public class HangFireConfiguration
    {
        private readonly IConfiguration _configuration;

        public HangFireConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureHangfireServices(IServiceCollection services)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                      .UseSimpleAssemblyNameTypeSerializer()
                      .UseRecommendedSerializerSettings()
                      .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                      {
                          CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                          SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                          QueuePollInterval = TimeSpan.Zero,
                          UseRecommendedIsolationLevel = true,
                          UsePageLocksOnDequeue = true,
                          DisableGlobalLocks = true
                      });
            });
            services.AddHangfireServer();
        }

        public void ConfigureHangfirePipeline(WebApplication app)
        {
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate<PrescriptionsService>(
                "update-expired-prescriptions",
                service => service.UpdateExpiredPrescriptions(),
                Cron.Daily // Every day at midnight
            );
            RecurringJob.AddOrUpdate<SMTP_Visit_Reminder>(
              "appointment-reminder-job",
                  service => service.SendAppointmentReminders(),
                 Cron.Daily(9, 0) // Every day at 9 AM
            );
        }
    }
}