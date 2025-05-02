using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediCore.Services.Interfaces;
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
    //[HttpGet("diagnoses/patient/{patientId}")]
    //public ActionResult GetDiagnosesByPatientId(int patientId)
    //{
    //    var diagnoses = _diagnosesService.GetDiagnosesByPatientId(patientId);
    //    if (diagnoses == null || !diagnoses.Any())
    //    {
    //        return NotFound();
    //    }
    //    return Ok(diagnoses);
    //}
}
