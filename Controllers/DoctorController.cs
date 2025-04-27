using MediCore.Core;
using MediCore.DTOs.DoctorDTOs;
using MediCore.Services.Implementations;
using MediCore.Services.Interaces;
using MediCore.Services.Interfaces;
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
        [HttpGet("doctors")]
        public ActionResult<ApiResponse<List<DoctorAllDTO>>> GetAllDoctors()
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
                return null;
            }
        }
        // GET DOCTOR BY ID
        [HttpGet("doctors/{id}")]
        public ActionResult<ApiResponse<DoctorByIdDTO>> GetDoctorById(int id)
        {
            var response = _doctorService.GetDoctorById(id);

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
                return null;
            }
        }


        // UPDATE DOCTOR BY ID
        [HttpPut("doctors/{id}")]
        public ActionResult<ApiResponse<DoctorByIdDTO>> UpdateDoctor(int id, DoctorUpdateDTO doctorUpdateDTO)
        {
            var response = _doctorService.UpdateDoctor(id, doctorUpdateDTO);

            if (response.Status == 200)
            {
                return Ok(response);
            }
            else if (response.Status == 400)
            {
                return BadRequest(response);

            }
            else if (response.Status == 404)
            {
                return NotFound(response);
            }
            else
            {
                return null;
            }
        }

    }
}
