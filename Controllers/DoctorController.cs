using MediCore.Core;
using MediCore.DTOs.DoctorDTOs;
using MediCore.Services.Interaces;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.Controllers
{
    [Route("api/")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctor _doctorService;
        public DoctorController(IDoctor doctorService)
        {
            _doctorService = doctorService;
        }

        // GET ALL DOCTORS
        [HttpGet("get-all-doctors")]
        public ActionResult<ApiResponse<List<DoctorDTO>>> GetAllDoctors()
        {
            var response = _doctorService.GetAllDoctors();

            if (response.Status == 200)
            {
                return Ok(response);
            }
            else if (response.Status == 404)
            {
                return NotFound(response);
            }
            else
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
