using MediCore.Services.Interfaces;
using MediCore.Core;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReports _reportsService;
        public ReportsController(IReports reportsService)
        {
            _reportsService = reportsService;
        }

        // GET APPOINTMENT STATS
        [HttpGet("appointments")]
        public ActionResult<ApiResponse<object>> GetAppointmentStats()
        {
            var response = _reportsService.GetAppointmentStats();
            if (response.Status == 404)
            {
                return NotFound(response);
            }
            else if (response.Status == 200)
            {
                return Ok(response);
            }
            else
            {
                return null;
            }
        }

        // GET DIAGNOSIS STATS
        [HttpGet("diagnoses")]
        public ActionResult<ApiResponse<object>> GetDiagnosisStats()
        {
            var response = _reportsService.GetDiagnosisStats();
            if (response.Status == 404)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }

        // GET DEPARTMENT LOAD REPORT
        [HttpGet("departments")]
        public ActionResult<ApiResponse<object>> GetDepartmentLoadReport()
        {
            var response = _reportsService.GetDepartmentLoadReport();
            if (response.Status == 404)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }

        // GET DOCTOR ACTIVITY REPORT
        [HttpGet("doctors")]
        public ActionResult<ApiResponse<object>> GetDoctorActivityReport()
        {
            var response = _reportsService.GetDoctorActivityReport();
            if (response.Status == 404)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}