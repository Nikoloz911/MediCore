using MediCore.Core;
using MediCore.DTOs.AppointmentsDTOs;
using MediCore.Enums;
using MediCore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.Controllers;

[Route("api/")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointments _appointmentsService;
    public AppointmentsController(IAppointments appointmentsService)
    {
        _appointmentsService = appointmentsService;
    }
    // GET ALL APPOINTMENTS BY FILTERS
    [HttpGet("appointments")]
    public ActionResult<ApiResponse<List<GetAppointmentsDTO>>> GetAppointments(
     [FromQuery] int? doctorId,
     [FromQuery] int? patientId,
     [FromQuery] int? departmentId,
     [FromQuery] APPOINTMENT_STATUS? status,
     [FromQuery] APPOINTMENT_TYPE? visitType,
     [FromQuery] DateTime? date)
    {
        var response = _appointmentsService.GetAppointments(
            doctorId, patientId, departmentId, status, visitType, date);

        if (response.Status == 404)                 // NOT FOUND
        {
            return NotFound(response);
        }
        else if (response.Status == 200)
        {
            return Ok(response);                    //  OK
        }

        return null;
    }
    // GET APPOINTMENT BY ID
    [HttpGet("appointments/{id}")]
    public ActionResult<ApiResponse<GetAppointmentsByIdDTO>> GetAppointmentById(int id)
    {
        var response = _appointmentsService.GetAppointmentById(id);

        if (response.Status == 404)  // NOT FOUND
        {
            return NotFound(response);  
        }
        else if (response.Status == 200)  // OK
        {
            return Ok(response); 
        }
        return null;
    }
    // ADD APPOINTMENT
    [HttpPost("appointments")]
    public ActionResult<ApiResponse<AddAppointmentResponseDTO>> AddAppointment([FromBody] AddAppointmentDTO dto)
    {
        var response = _appointmentsService.AddAppointment(dto);

        if (response.Status == 200)
        {
            return Ok(response);
        }
        else if (response.Status == 400) // BAD REQUEST
        {
            return BadRequest(response);
        }
        return null;
    }



    //[HttpPut("appointments/{id}")]
    //[HttpDelete("appointments/{id}")]
    //[HttpGet("appointments/doctor/{doctorId}")]
    //[HttpGet("appointments/patient/{patientId}")]

}
