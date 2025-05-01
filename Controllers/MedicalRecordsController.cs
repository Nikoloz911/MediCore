using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediCore.Services.Interfaces;
using MediCore.Models;
namespace MediCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicalRecordsController : ControllerBase
{
    private readonly IMedicalRecords _medicalRecordsService;
    public MedicalRecordsController(IMedicalRecords medicalRecordsService)
    {
        _medicalRecordsService = medicalRecordsService;
    }



//    GET /api/medical-records/patient/{patientId
//} - პაციენტის სამედიცინო ჩანაწერები
//GET /api/medical-records/{id} -კონკრეტული ჩანაწერის დეტალები
//POST /api/medical-records - ახალი ჩანაწერის შექმნა (Doctor)
//PUT /api/medical-records/{id} -ჩანაწერის განახლება(Doctor)
}
