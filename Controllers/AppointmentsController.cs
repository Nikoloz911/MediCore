using AutoMapper;
using FluentValidation;
using MediCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.Controllers;

[Route("api/")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IValidator _validator;
    private readonly IMapper _mapper;
    public AppointmentsController(DataContext context, IValidator validator, IMapper mapper)
    {
        _context = context;
        _validator = validator;
        _mapper = mapper;
    }
    [HttpGet("appointments")]


    //[HttpGet("appointments/{id}")]
    //[HttpPost("appointments")]
    //[HttpPut("appointments/{id}")]
    //[HttpDelete("appointments/{id}")]
    //[HttpGet("appointments/doctor/{doctorId}")]
    //[HttpGet("appointments/patient/{patientId}")]

}
