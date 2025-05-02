using MediCore.Core;
using MediCore.DTOs.AppointmentsDTOs;
using MediCore.Enums;
using MediCore.Services.Implementations;
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
     [FromQuery] DateOnly? date)
    {
        var response = _appointmentsService.GetAppointments(
            doctorId, patientId, departmentId, status, visitType, date);

        if (response.Status == 404)                   // NOT FOUND
        {
            return NotFound(response);
        }
        else if (response.Status == 200)
        {
            return Ok(response);                       //  OK
        }

        return null;
    }
    // GET APPOINTMENT BY ID
    [HttpGet("appointments/{id}")]
    public ActionResult<ApiResponse<GetAppointmentsByIdDTO>> GetAppointmentById(int id)
    {
        var response = _appointmentsService.GetAppointmentById(id);

        if (response.Status == 404)              // NOT FOUND
        {
            return NotFound(response);  
        }
        else if (response.Status == 200)            // OK
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
        if (response.Status == 200)                  // OK
        {
            return Ok(response);
        }
        else if (response.Status == 400)             // BAD REQUEST
        {
            return BadRequest(response);
        }
        else if (response.Status == 409)              // CONFLICT
        {
            return Conflict(response);            
        }
        return null;
    }
    // UPDATE APPOINTMENT BY ID
    [HttpPut("appointments/{id}")]
    public ActionResult<ApiResponse<UpdateAppointmentDTO>> UpdateAppointment(int id, UpdateAppointmentDTO updateDto)
    {
        var response = _appointmentsService.UpdateAppointment(id, updateDto);
        if (response.Status == StatusCodes.Status200OK)             // OK
        {
            return Ok(response);
        }
        else if (response.Status == StatusCodes.Status404NotFound)      // NOT FOUND
        {
            return NotFound(response);
        }
        else if (response.Status == StatusCodes.Status400BadRequest)        // BAD REQUEST
        {
            return BadRequest(response);
        }
        else if (response.Status == 409)              // CONFLICT
        {
            return Conflict(response);
        }
        else
        {
            return null;
        }
    }
    [HttpDelete("appointments/{id}")]
    public ActionResult<ApiResponse<string>> DeleteAppointment(int id)
    {
        var response = _appointmentsService.DeleteAppointment(id);
        if (response.Status == StatusCodes.Status200OK)             // OK
        {
            return Ok(response);
        }
        else if (response.Status == StatusCodes.Status404NotFound)      // NOT FOUND
        {
            return NotFound(response);
        }
        else
        {
            return null;
        }
    }
    // GET APPOINTMENTS BY DOCTOR ID
    [HttpGet("appointments/doctor/{doctorId}")]
    public ActionResult<ApiResponse<List<GetDoctorsAppointmentsDTO>>> GetAppointmentsByDoctorId(int doctorId)
    {
        var response = _appointmentsService.GetAppointmentsByDoctorId(doctorId);
        if (response.Status == StatusCodes.Status200OK)                          // OK
        {
            return Ok(response);
        }
        else if (response.Status == StatusCodes.Status404NotFound)                // NOT FOUND
        {
            return NotFound(response);
        }
        else
        {
            return null;
        }
    }

    // GET APPOINTMENTS BY PATIENT ID
    [HttpGet("appointments/patient/{patientId}")]
    public ActionResult<ApiResponse<List<GetPatientsAppointmentsDTO>>> GetAppointmentsByPatientId(int patientId)
    {
        var response = _appointmentsService.GetAppointmentsByPatientId(patientId);
        if (response.Status == StatusCodes.Status200OK)                               // OK 
        {
            return Ok(response);
        }
        else if (response.Status == StatusCodes.Status404NotFound)                  // NOT FOUND
        {
            return NotFound(response);
        }
        else
        {
            return null;
        }
    }
}
