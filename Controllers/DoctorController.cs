using MediCore.Core;
using MediCore.DTOs.DoctorDTOs;
using MediCore.Services.Implementations;
using MediCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        // GET DOCTOR BY DEPARTMENTS ID
        [HttpGet("doctors/department/{departmentId}")]
        public ActionResult<ApiResponse<List<DoctorsByDepartmentDTO>>> GetDoctorsByDepartment(int departmentId)
        {
            var response = _doctorService.GetDoctorsByDepartment(departmentId);
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
                return StatusCode(500, response);
            }
        }
        // GET DOCTOR SCHEDULE BY DOCTOR ID
        [HttpGet("doctors/schedule/{doctorId}")]
        public ActionResult<ApiResponse<DoctorScheduleDTO>> GetDoctorSchedule(int doctorId)
        {
            var response = _doctorService.GetDoctorSchedule(doctorId);
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
        [Authorize(Policy = "AdminOrDoctor")]
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
