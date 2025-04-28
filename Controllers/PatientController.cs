using MediCore.Services.Interfaces;
using MediCore.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediCore.Core;
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
    // [Authorize(Policy = "AdminOrDoctor")]
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
    // [Authorize(Policy = "AdminOrDoctorOrOwner")]
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

}
