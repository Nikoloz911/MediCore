using MediCore.Services.Interfaces;
using MediCore.Data;
using MediCore.Core;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Services.Implementations
{
    public class ReportsService : IReports
    {
        private readonly DataContext _context;

        public ReportsService(DataContext context)
        {
            _context = context;
        }
        // GET APPOINTMENT STATS
        public ApiResponse<object> GetAppointmentStats()
        {
            var grouped = _context.Appointments
                .GroupBy(a => a.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(g => g.Date)
                .ToList();

            if (!grouped.Any())
            {
                return new ApiResponse<object>
                {
                    Status = 404,
                    Message = "No appointment statistics found.",
                    Data = null
                };
            }

            return new ApiResponse<object>
            {
                Status = 200,
                Message = "Appointment statistics fetched successfully.",
                Data = grouped
            };
        }
        // GET DIAGNOSIS STATS
        public ApiResponse<object> GetDiagnosisStats()
        {
            var grouped = _context.Diagnoses
                .GroupBy(d => d.ICD10Code)
                .Select(g => new
                {
                    Code = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Count)
                .ToList();

            if (!grouped.Any())
            {
                return new ApiResponse<object>
                {
                    Status = 404,
                    Message = "No diagnosis statistics found.",
                    Data = null
                };
            }

            return new ApiResponse<object>
            {
                Status = 200,
                Message = "Diagnosis statistics fetched successfully.",
                Data = grouped
            };
        }
        // GET DEPARTMENT LOAD REPORT
        public ApiResponse<object> GetDepartmentLoadReport()
        {
            var grouped = _context.Appointments
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.Department)
                .Where(a => a.Doctor != null && a.Doctor.Department != null)
                .GroupBy(a => a.Doctor.Department.DepartmentType)
                .Select(g => new
                {
                    Department = g.Key.ToString(),
                    VisitCount = g.Count()
                })
                .ToList();

            if (!grouped.Any())
            {
                return new ApiResponse<object>
                {
                    Status = 404,
                    Message = "No department load data found.",
                    Data = null
                };
            }

            return new ApiResponse<object>
            {
                Status = 200,
                Message = "Department load report generated successfully.",
                Data = grouped
            };
        }
        // GET DOCTOR ACTIVITY REPORT
        public ApiResponse<object> GetDoctorActivityReport()
        {
            var activity = _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Appointments)
                .Select(d => new
                {
                    DoctorName = d.User.FirstName + " " + d.User.LastName,
                    AppointmentCount = d.Appointments.Count
                })
                .OrderByDescending(d => d.AppointmentCount)
                .ToList();

            if (!activity.Any())
            {
                return new ApiResponse<object>
                {
                    Status = 404,
                    Message = "No doctor activity data found.",
                    Data = null
                };
            }

            return new ApiResponse<object>
            {
                Status = 200,
                Message = "Doctor activity report generated successfully.",
                Data = activity
            };
        }
    }
}