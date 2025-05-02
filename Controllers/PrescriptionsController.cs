using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediCore.Services.Interfaces;
using MediCore.Models;
using System;
namespace MediCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptions _prescriptionsService;
    public PrescriptionsController(IPrescriptions prescriptionsService)
    {
        _prescriptionsService = prescriptionsService;
    }



//    GET /api/prescriptions/patient/{patientId
//} - პაციენტის რეცეპტები
//GET /api/prescriptions/{id} -კონკრეტული რეცეპტის დეტალები
//POST /api/prescriptions - ახალი რეცეპტის შექმნა (Doctor)
//PUT /api/prescriptions/{id} -რეცეპტის განახლება(Doctor)
//GET / api / prescriptions / active / patient /{ patientId}
//-პაციენტის აქტიური რეცეპტები
}
