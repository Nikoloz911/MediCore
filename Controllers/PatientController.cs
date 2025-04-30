using MediCore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediCore.DTOs.PatientDTOs;

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