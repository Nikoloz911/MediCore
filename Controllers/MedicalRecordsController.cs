using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediCore.Services.Interfaces;
using MediCore.Models;
using Microsoft.AspNetCore.Authorization;
using MediCore.Core;
using MediCore.DTOs.MedicalRecordsDTOs;
namespace MediCore.Controllers;

[Route("api/")]
[ApiController]
public class MedicalRecordsController : ControllerBase
{
    private readonly IMedicalRecords _medicalRecordsService;
    public MedicalRecordsController(IMedicalRecords medicalRecordsService)
    {
        _medicalRecordsService = medicalRecordsService;
    }

    [HttpGet("medical-records/patient/{patientId}")]
    public ActionResult<ApiResponse<List<GetPatientsMedicalRecordsDTO>>> GetPatientMedicalRecords(int patientId)
    {
        var result = _medicalRecordsService.GetMedicalRecordsByPatientId(patientId);   
         if (result.Status == 200)                      // OK
        {
            return Ok(result);
        }
        else if (result.Status == 404)                  // NOT FOUND
        {
            return NotFound(result);
        }
        else
        {
            return null;
        }
    }
    // GET MEDICAL RECORD BY ID
    [HttpGet("medical-records/{id}")]
    public ActionResult<ApiResponse<GetMedicalRecordsDTO>> GetMedicalRecordById(int id)
    {
        var result = _medicalRecordsService.GetMedicalRecordById(id);
        if (result.Status == 200)                    // OK
        {
            return Ok(result);
        }
        else if (result.Status == 404)              // NOT FOUND
        {
            return NotFound(result);
        }
        else
        {
            return null;
        }
    }
    // ADD MEDICAL RECORD
    [HttpPost("medical-records")]
    [Authorize(Policy = "DoctorOnly")]
    public ActionResult<ApiResponse<MedicalRecordResponseDTO>> CreateMedicalRecord([FromBody] CreateMedicalRecordDTO dto)
    {
        var result = _medicalRecordsService.CreateMedicalRecord(dto);

        if (result.Status == 200)  // OK
        {
            return Ok(result);
        }
        else if (result.Status == 400)  // BAD REQUEST
        {
            return BadRequest(result);
        }
        else
        {
            return null;
        }
    }
    // UPDATE MEDICAL RECORD
    [HttpPut("medical-records/{id}")]
    [Authorize(Policy = "DoctorOnly")]
    public ActionResult<ApiResponse<MedicalRecordResponseDTO>> UpdateMedicalRecord(int id, [FromBody] UpdateMedicalRecordDTO dto)
    {
        var result = _medicalRecordsService.UpdateMedicalRecord(id, dto);

        if (result.Status == 200)  // OK
        {
            return Ok(result);
        }
        else if (result.Status == 404)  // NOT FOUND
        {
            return NotFound(result);
        }
        else if (result.Status == 400)  // BAD REQUEST
        {
            return BadRequest(result);
        }
        else
        {
            return null;
        }
    }
}
