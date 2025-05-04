using AutoMapper;
using FluentValidation;
using MediCore.Core;
using MediCore.Data;
using MediCore.DTOs.DiagnosesDTOs;
using MediCore.Models;
using MediCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace MediCore.Services.Implementations;
public class DiagnosesService : IDiagnoses
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<AddDiagnosesDTO> _validator;
    private readonly IValidator<UpdateDiagnosesDTO> _updateValidator;
    public DiagnosesService(DataContext context, IMapper mapper, IValidator<AddDiagnosesDTO> validator, IValidator<UpdateDiagnosesDTO> updateValidator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
        _updateValidator = updateValidator;
    }

    // GET DIAGNOSIS BY PATIENT ID
    public ApiResponse<List<GetPatientDiagnosesDTO>> GetDiagnosesByPatientId(int patientId)
    {
        var diagnoses = _context.Diagnoses
            .Include(d => d.MedicalRecord)
                .ThenInclude(mr => mr.Patient)
            .Where(d => d.MedicalRecord.Patient.Id == patientId)
            .ToList();

        if (diagnoses == null || diagnoses.Count == 0)
        {
            return new ApiResponse<List<GetPatientDiagnosesDTO>>
            {
                Status = 404,
                Message = "No diagnoses found for this patient.",
                Data = new List<GetPatientDiagnosesDTO>()
            };
        }

        var mapped = _mapper.Map<List<GetPatientDiagnosesDTO>>(diagnoses);

        return new ApiResponse<List<GetPatientDiagnosesDTO>>
        {
            Status = 200,
            Message = "Diagnoses retrieved successfully.",
            Data = mapped
        };
    }
    // GET DIAGNOSES BY MEDICAL RECORD ID
    public ApiResponse<List<GetMedicalRecordsDiagnosesDTO>> GetDiagnosesByMedicalRecordId(int recordId)
    {
        var diagnoses = _context.Diagnoses
            .Include(d => d.MedicalRecord)
                .ThenInclude(mr => mr.Patient)
            .Where(d => d.MedicalRecord.Id == recordId)
            .ToList();

        if (diagnoses == null || diagnoses.Count == 0)
        {
            return new ApiResponse<List<GetMedicalRecordsDiagnosesDTO>>
            {
                Status = 404,
                Message = "No diagnoses found for this medical record.",
                Data = new List<GetMedicalRecordsDiagnosesDTO>()
            };
        }

        var mapped = _mapper.Map<List<GetMedicalRecordsDiagnosesDTO>>(diagnoses);

        return new ApiResponse<List<GetMedicalRecordsDiagnosesDTO>>
        {
            Status = 200,
            Message = "Diagnoses retrieved successfully for the medical record.",
            Data = mapped
        };
    }
    // ADD DIAGNOSES
    public ApiResponse<AddDiagnosesResponseDTO> AddDiagnosis(AddDiagnosesDTO newDiagnosis)
    {
        // VALIDATE
        if (newDiagnosis == null || newDiagnosis.MedicalRecordId == 0)
        {
            return new ApiResponse<AddDiagnosesResponseDTO>
            {
                Status = 400,
                Message = "Invalid input!.",
                Data = null
            };
        }
        // VALIDATE
        var validationResult = _validator.Validate(newDiagnosis);
        if (!validationResult.IsValid)
        {
            return new ApiResponse<AddDiagnosesResponseDTO>
            {
                Status = 400,
                Message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)),
                Data = null
            };
        }
        // CHECK IF MEDICAL RECORD EXISTS
        var medicalRecord = _context.MedicalRecords
            .FirstOrDefault(mr => mr.Id == newDiagnosis.MedicalRecordId);
        if (medicalRecord == null)
        {
            return new ApiResponse<AddDiagnosesResponseDTO>
            {
                Status = 404,
                Message = "Medical record not found.",
                Data = null
            };
        }
        // MAP TO DTO
        var diagnosis = _mapper.Map<Diagnoses>(newDiagnosis);
        diagnosis.MedicalRecord = medicalRecord;

        _context.Diagnoses.Add(diagnosis);
        _context.SaveChanges();
        // MAP TO DTO
        var responseDTO = _mapper.Map<AddDiagnosesResponseDTO>(diagnosis);
        return new ApiResponse<AddDiagnosesResponseDTO>
        {
            Status = 200,
            Message = "Diagnosis successfully created.",
            Data = responseDTO
        };
    }
    // UPDATE DIAGNOSES
    public ApiResponse<UpdateDiagnosesResponseDTO> UpdateDiagnosis(int id, UpdateDiagnosesDTO dto)
    {
        // VALIDATE
        var validationResult = _updateValidator.Validate(dto);
        if (!validationResult.IsValid)
        {
            return new ApiResponse<UpdateDiagnosesResponseDTO>
            {
                Status = 400,
                Message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)),
                Data = null
            };
        }
        var existingDiagnosis = _context.Diagnoses.FirstOrDefault(d => d.Id == id);
        if (existingDiagnosis == null)
        {
            return new ApiResponse<UpdateDiagnosesResponseDTO>
            {
                Status = 404,
                Message = "Diagnosis not found.",
                Data = null
            };
        }

        _mapper.Map(dto, existingDiagnosis);
        _context.SaveChanges();

        var responseDto = _mapper.Map<UpdateDiagnosesResponseDTO>(existingDiagnosis);
        return new ApiResponse<UpdateDiagnosesResponseDTO>
        {
            Status = 200,
            Message = "Diagnosis updated successfully.",
            Data = responseDto
        };
    }
}
