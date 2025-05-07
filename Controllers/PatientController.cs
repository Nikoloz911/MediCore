using MediCore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediCore.DTOs.PatientDTOs;
using MediCore.Core;

namespace MediCore.Controllers;

[Route("api/")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatient _patientService;

    public PatientController(IPatient patientService)
    {
        _patientService = patientService;
    }

    // GET ALL PATIENTS
    [HttpGet("patients")]
    [Authorize(Policy = "AdminOrDoctor")]
    public ActionResult GetAllPatients()
    {
        var response = _patientService.GetAllPatients();
        if (response.Status == StatusCodes.Status200OK)  // OK
        {
            return Ok(response);
        }
        else if (response.Status == StatusCodes.Status404NotFound) // NOT FOUND
        {
            return NotFound(response);
        }
        else
        {
            return null;
        }
    }

    // GET PATIENT BY ID
    [HttpGet("patients/{id}")]
    [Authorize(Policy = "AdminOrDoctor")]
    public ActionResult GetPatientById(int id)
    {
        var response = _patientService.GetPatientById(id);
        if (response.Status == StatusCodes.Status200OK)  // OK
        {
            return Ok(response);
        }
        else if (response.Status == StatusCodes.Status404NotFound)  // NOT FOUND
        {
            return NotFound(response);
        }
        else
        {
            return null;
        }
    }

    // ADD NEW PATIENT
    [HttpPost("patients")]
    [Authorize(Policy = "AdminOnly")]
    public ActionResult AddPatient(PatientAddDTO patientDto)
    {
        var response = _patientService.AddPatient(patientDto);

        if (response.Status == StatusCodes.Status200OK)          //  OK
        {
            return Ok(response);
        }
        else if (response.Status == StatusCodes.Status400BadRequest)        // BAD REQUEST
        {
            return BadRequest(response);
        }
        else if (response.Status == StatusCodes.Status409Conflict)        // CONFLICT
        {
            return Conflict(response);
        }
        else
        {
            return null;
        }
    }
    // ADD PATIENT WITH USER ID
    [HttpPost("patients/{userId}/user")]
    public ActionResult<ApiResponse<AddPatientUserResponseDTO>> AddPatientByUserId(int userId, [FromBody] AddPatientUserDTO patientDto)
    {
        if (patientDto == null)
        {
            return BadRequest(new ApiResponse<AddPatientUserResponseDTO>
            {
                Status = StatusCodes.Status400BadRequest,
                Message = "Request body cannot be null.",
                Data = null
            });
        }

        if (userId != patientDto.UserId)
        {
            return BadRequest(new ApiResponse<AddPatientUserResponseDTO>
            {
                Status = StatusCodes.Status400BadRequest,
                Message = "User ID in URL does not match the one in the request body.",
                Data = null
            });
        }

        if (patientDto.UserId <= 0)
        {
            return BadRequest(new ApiResponse<AddPatientUserResponseDTO>
            {
                Status = StatusCodes.Status400BadRequest,
                Message = "User ID in the body is invalid. It must be a positive number.",
                Data = null
            });
        }

        var response = _patientService.AddPatientByUserId(patientDto);

        return response.Status switch
        {
            StatusCodes.Status200OK => Ok(response),
            StatusCodes.Status400BadRequest => BadRequest(response),
            StatusCodes.Status409Conflict => Conflict(response),
            StatusCodes.Status404NotFound => NotFound(response),
            StatusCodes.Status500InternalServerError => StatusCode(StatusCodes.Status500InternalServerError, response),
            _ => StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<AddPatientUserResponseDTO>
            {
                Status = StatusCodes.Status500InternalServerError,
                Message = "An unexpected error occurred.",
                Data = null
            })
        };
    }


    // GET PATIENT HISTORY BY PATIENT ID
    [HttpGet("patients/{id}/medical-history")]
    public ActionResult DeletePatient(int id) {
        var response = _patientService.GetPatientHistory(id);
        if (response.Status == StatusCodes.Status200OK)  // OK
        {
            return Ok(response);
        }
        else if (response.Status == StatusCodes.Status404NotFound)  // NOT FOUND
        {
            return NotFound(response);
        }
        else
        {
            return null;
        }
    }
}