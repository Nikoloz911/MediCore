using FluentValidation.AspNetCore;
using MediCore.Data;
using MediCore.Services.Implementations;
using MediCore.Services.Interfaces;

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
            services.AddScoped<IPatient, PatientService>();
            services.AddScoped<IDepartment, DepartmentService>();
            services.AddScoped<IAppointments, AppointmentsService>();
            services.AddScoped<IMedicalRecords, MedicalRecordsService>();
            services.AddScoped<IDiagnoses, DiagnosesService>();
            services.AddScoped<IPrescriptions, PrescriptionsService>();
            services.AddScoped<IMedications, MedicationsService>();
            services.AddScoped<ILabTests_Results, LabTests_ResultsService>();
            services.AddScoped<IReports, ReportsService>();
            services.AddAutoMapper(typeof(Program));
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
        }
    }
}
