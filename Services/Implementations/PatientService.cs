using AutoMapper;
using MediCore.Core;
using MediCore.Data;
using MediCore.DTOs.PatientDTOs;
using MediCore.Services.Interfaces;

namespace MediCore.Services.Implementations;
public class PatientService : IPatient
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public PatientService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET ALL PATIENTS
    public ApiResponse<List<PatientGetDTO>> GetAllPatients()
    {
        var patients = _context.Patients.ToList();
        if (patients == null || !patients.Any())
        {
            return new ApiResponse<List<PatientGetDTO>>
            {
                Status = StatusCodes.Status404NotFound,
                Message = "No patients found.",
                Data = null
            };
        }

        var patientDtos = _mapper.Map<List<PatientGetDTO>>(patients);
        return new ApiResponse<List<PatientGetDTO>>
        {
            Status = StatusCodes.Status200OK,
            Message = "Patients retrieved successfully.",
            Data = patientDtos
        };
    }

    // GET PATIENT BY ID
    public ApiResponse<PatientGetByIdDTO> GetPatientById(int id)
    {
        var patient = _context.Patients.FirstOrDefault(p => p.Id == id);
        if (patient == null)
        {
            return new ApiResponse<PatientGetByIdDTO>
            {
                Status = StatusCodes.Status404NotFound,
                Message = $"Patient with ID {id} not found.",
                Data = null
            };
        }
        var patientDto = _mapper.Map<PatientGetByIdDTO>(patient);
        return new ApiResponse<PatientGetByIdDTO>
        {
            Status = StatusCodes.Status200OK,
            Message = "Patient retrieved successfully.",
            Data = patientDto
        };
    }

}
