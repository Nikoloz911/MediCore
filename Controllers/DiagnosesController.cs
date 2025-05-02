using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediCore.Services.Interfaces;
using MediCore.Core;
using MediCore.DTOs.DiagnosesDTOs;
namespace MediCore.Controllers;

[Route("api/")]
[ApiController]
public class DiagnosesController : ControllerBase
{
    private readonly IDiagnoses _diagnosesService;

    public DiagnosesController(IDiagnoses diagnosesService)
    {
        _diagnosesService = diagnosesService;
    }
    // GET DIAGNOSES BY PATIENT ID
    [HttpGet("diagnoses/patient/{patientId}")]
    public ActionResult<ApiResponse<List<GetPatientDiagnosesDTO>>> GetDiagnosesByPatientId(int patientId)
    {
        var response = _diagnosesService.GetDiagnosesByPatientId(patientId);

        if (response.Status == 200)         /// OK
        {
            return Ok(response);
        }
        else if (response.Status == 404)        // NOT FOUND
        {
            return NotFound(response);
        }
        else
        {
            return null;
        }
    }
    // GET DIAGNOSES BY MEDICAL RECORD ID
    [HttpGet("diagnoses/record/{recordId}")]
    public ActionResult<ApiResponse<List<GetMedicalRecordsDiagnosesDTO>>> GetDiagnosesByMedicalRecordId(int recordId)
    {
        var response = _diagnosesService.GetDiagnosesByMedicalRecordId(recordId);

        if (response.Status == 200)             /// OK
        {
            return Ok(response);
        }
        else if (response.Status == 404)        /// NOT FOUND
        {
            return NotFound(response);
        }
        else
        {
            return null;
        }
    }
    // ADD DIAGNOSES
    [HttpPost("diagnoses")]
    public ActionResult<ApiResponse<AddDiagnosesResponseDTO>> AddDiagnosis([FromBody] AddDiagnosesDTO newDiagnosis)
    {
        var response = _diagnosesService.AddDiagnosis(newDiagnosis);

        if (response.Status == 200)             // OK
        {
            return CreatedAtAction(nameof(AddDiagnosis), new { id = response.Data.Id }, response);
        }
        else if (response.Status == 404)  // Not Found 
        {
            return NotFound(response);
        }
        else if (response.Status == 400)  // Bad Request 
        {
            return BadRequest(response);
        }
        else
        {
            return null;
        }
    }
    // UPDATE DIAGNOSES
    [HttpPut("diagnoses/{id}")]
    public ActionResult<ApiResponse<UpdateDiagnosesResponseDTO>> UpdateDiagnosis(int id, [FromBody] UpdateDiagnosesDTO dto)
    {
        var response = _diagnosesService.UpdateDiagnosis(id, dto);

        if (response.Status == 200)             // OK
        {
            return Ok(response);
        }
        else if (response.Status == 404)        // Not Found
        {
            return NotFound(response);
        }
        else if (response.Status == 400)        // Bad Request
        {
            return BadRequest(response);
        }
        else
        {
            return null;
        }
    }
}
