using MediCore.Core;

namespace MediCore.Services.Interfaces
{
    public interface IReports
    {
        ApiResponse<object> GetAppointmentStats();
        ApiResponse<object> GetDiagnosisStats();
        ApiResponse<object> GetDepartmentLoadReport();
        ApiResponse<object> GetDoctorActivityReport();
    }
}
