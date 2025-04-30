using AutoMapper;
using MediCore.Core;
using MediCore.DTOs.DepartmentsDTOs;
using MediCore.Services.Implementations;
using MediCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.Controllers
{
    [Route("api/")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartment _departmentService;
        public DepartmentsController(IDepartment departmentService)
        {
            _departmentService = departmentService;
        }


        // GET ALL DEPARTMENTS
        [HttpGet("departments")]
        public ActionResult<ApiResponse<List<DepartmentAllDTO>>> GetAllDepartments()
        {
            var response = _departmentService.GetAllDepartments();
            if (response.Status == 200) // OK
            {
                return Ok(response);
            }
            else if (response.Status == 404) // NOT FOUND
            {
                return NotFound(response);
            }
            else
            {
                return null;
            }
        }

        // ADD NEW DEPARTMENT
        [HttpPost("departments")]
        // [Authorize(Policy = "AdminOnly")] 
        public ActionResult<ApiResponse<DepartmentAllDTO>> CreateDepartment([FromBody] DepartmentAddDTO departmentDto)
        {
            var response = _departmentService.CreateDepartment(departmentDto);

            if (response.Status == 200)     // OK
            {
                return StatusCode(response.Status, response); 
            }
            else if (response.Status == 400)            // BAD REQUEST
            {
                return BadRequest(response);
            }
            else if (response.Status == 404)              // NOT FOUND
            {
                return NotFound(response);
            }
            else if (response.Status == 409)               // CONFLICT 
            {
                return Conflict(response);
            }
            else
            {
                return null;
            }
        }


        // UPDATE DEPARTMENT
        [HttpPut("departments/{id}")]
        // [Authorize(Policy = "AdminOnly")] 
        public ActionResult<ApiResponse<DepartmentAllDTO>> UpdateDepartment(int id, [FromBody] DepartmentUpdateDTO departmentDto)
        {
            var response = _departmentService.UpdateDepartment(id, departmentDto);
            if (response.Status == 200)           // OK
            {
                return Ok(response);
            }
            else if (response.Status == 404)       // NOT FOUND
            {
                return NotFound(response);
            }
            else if (response.Status == 400)      // BAD REQUEST
            {
                return BadRequest(response);
            }
            else if (response.Status == 409)      // CONFLICT
            {
                return Conflict(response);
            }
            else
            {
                return null;
            }
        }

        // DELETE DEPARTMENT
        [HttpDelete("departments/{id}")]
        // [Authorize(Policy = "AdminOnly")] 
        public ActionResult<ApiResponse<bool>> DeleteDepartment(int id)
        {
            var response = _departmentService.DeleteDepartment(id);
            if (response.Status == 200) // OK
            {
                return Ok(response);
            }
            else if (response.Status == 404) // NOT FOUND
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