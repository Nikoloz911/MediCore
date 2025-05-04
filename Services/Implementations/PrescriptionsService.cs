using AutoMapper;
using FluentValidation;
using MediCore.Core;
using MediCore.Data;
using MediCore.Models;
using MediCore.DTOs.DiagnosesDTOs;
using MediCore.DTOs.PrescriptionsDTOs;
using MediCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediCore.Enums;
using MediCore.Validators;

namespace MediCore.Services.Implementations;
public class PrescriptionsService : IPrescriptions
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<AddPrescriptionsDTO> _addPrescriptionValidator;
    private readonly IValidator<UpdatePrescriptionDTO> _updatePrescriptionValidator;
    public PrescriptionsService(DataContext context, IMapper mapper, IValidator<AddPrescriptionsDTO> addPrescriptionValidator, IValidator<UpdatePrescriptionDTO> updatePrescriptionValidator)
    {
        _context = context;
        _mapper = mapper;
        _addPrescriptionValidator = addPrescriptionValidator;
        _updatePrescriptionValidator = updatePrescriptionValidator;
    }

    // GET PRESCTIPTIONS BY PATIENT ID
    public ApiResponse<List<GetPatientPrescriptionsDTO>> GetPrescriptionsByPatientId(int patientId)
    {
        var prescriptions = _context.Prescriptions
            .Where(p => p.MedicalRecord.PatientId == patientId)
            .Include(p => p.MedicalRecord)  
            .ThenInclude(mr => mr.Patient) 
            .ThenInclude(p => p.User)     
            .ToList();

        if (!prescriptions.Any())
        {
            return new ApiResponse<List<GetPatientPrescriptionsDTO>>
            {
                Status = 404,
                Message = "No prescriptions found for the given patient ID.",
                Data = null!
            };
        }
        // MAP TO DTO
        var result = _mapper.Map<List<GetPatientPrescriptionsDTO>>(prescriptions);
        return new ApiResponse<List<GetPatientPrescriptionsDTO>>
        {
            Status = 200,
            Message = "Prescriptions retrieved successfully.",
            Data = result
        };
    }
    // GET PRESCTIPTIONS BY ID
    public ApiResponse<GetPrescriptionsByIdDTO> GetPrescriptionById(int id)
    {
        var prescription = _context.Prescriptions
            .Include(p => p.MedicalRecord)
            .FirstOrDefault(p => p.Id == id);

        if (prescription == null)
        {
            return new ApiResponse<GetPrescriptionsByIdDTO>
            {
                Status = 404,
                Message = "Prescription not found.",
                Data = null!
            };
        }
        // MAP TO DTO
        var dto = _mapper.Map<GetPrescriptionsByIdDTO>(prescription);
        return new ApiResponse<GetPrescriptionsByIdDTO>
        {
            Status = 200,
            Message = "Prescription retrieved successfully.",
            Data = dto
        };
    }
    // ADD NEW PRESCRIPTION
    public ApiResponse<AddPrescriptionsResponseDTO> AddPrescription(AddPrescriptionsDTO dto)
    {
        // VALIDATE
        if (dto == null || dto.MedicalRecordId <= 0)
        {
            return new ApiResponse<AddPrescriptionsResponseDTO>
            {
                Status = 400,
                Message = "Invalid input!",
                Data = null!
            };
        }
        // VALIDATE DTO WITH VALIDATOR
        var validationResult = _addPrescriptionValidator.Validate(dto);
        if (!validationResult.IsValid)
        {
            return new ApiResponse<AddPrescriptionsResponseDTO>
            {
                Status = 400,
                Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                Data = null!
            };
        }
        // VALIDATE MEDICAL RECORD ID
        var medicalRecord = _context.MedicalRecords.FirstOrDefault(m => m.Id == dto.MedicalRecordId);
        if (medicalRecord == null)
        {
            return new ApiResponse<AddPrescriptionsResponseDTO>
            {
                Status = 404,
                Message = "Medical record not found.",
                Data = null!
            };
        }
        // VALIDATE STATUS
        if (!Enum.TryParse<PRESCRIPTION_STATUS>(dto.Status, true, out var status))
        {
            return new ApiResponse<AddPrescriptionsResponseDTO>
            {
                Status = 400,
                Message = "Invalid status value.",
                Data = null!
            };
        }
        // MAP TO PRESCRIPTION MODEL
        var prescription = _mapper.Map<Prescription>(dto);
        prescription.IssueDate = DateOnly.FromDateTime(DateTime.Now);
        _context.Prescriptions.Add(prescription);
        _context.SaveChanges();
        // MAP FOR RESPONSE
        var responseDto = _mapper.Map<AddPrescriptionsResponseDTO>(prescription);
        return new ApiResponse<AddPrescriptionsResponseDTO>
        {
            Status = 200,
            Message = "Prescription created successfully.",
            Data = responseDto
        };
    }
    // UPDATE PRESCRIPTION
    public ApiResponse<UpdatePrescriptionResponseDTO> UpdatePrescription(int id, UpdatePrescriptionDTO dto)
    {
        // VALIDATE
        if (dto.MedicalRecordId <= 0)
        {
            return new ApiResponse<UpdatePrescriptionResponseDTO>
            {
                Status = 400,
                Message = "Invalid input!",
                Data = null!
            };
        }
        // VALIDATE DTO WITH VALIDATOR
        var validationResult = _updatePrescriptionValidator.Validate(dto);
        if (!validationResult.IsValid)
        {
            return new ApiResponse<UpdatePrescriptionResponseDTO>
            {
                Status = 400,
                Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)),
                Data = null!
            };
        }
        // VALIDATE MEDICAL RECORD ID
        var prescription = _context.Prescriptions.FirstOrDefault(p => p.Id == id);
        if (prescription == null)
        {
            return new ApiResponse<UpdatePrescriptionResponseDTO>
            {
                Status = 404,
                Message = "Prescription not found.",
                Data = null!
            };
        }
        // VALIDATE MEDCIAL RECORD ID
        var medicalRecord = _context.MedicalRecords.FirstOrDefault(m => m.Id == dto.MedicalRecordId);
        if (medicalRecord == null)
        {
            return new ApiResponse<UpdatePrescriptionResponseDTO>
            {
                Status = 404,
                Message = "Medical record not found.",
                Data = null!
            };
        }
        // VALIDATE STATUS
        if (!Enum.TryParse<PRESCRIPTION_STATUS>(dto.Status, true, out var status))
        {
            return new ApiResponse<UpdatePrescriptionResponseDTO>
            {
                Status = 400,
                Message = "Invalid status value.",
                Data = null!
            };
        }
        prescription.Status = status;
        // MAP TO PRESCRIPTION MODEL
        _mapper.Map(dto, prescription);
        _context.SaveChanges();
        // MAP FOR RESPONSE
        var response = _mapper.Map<UpdatePrescriptionResponseDTO>(prescription);
        return new ApiResponse<UpdatePrescriptionResponseDTO>
        {
            Status = 200,
            Message = "Prescription updated successfully.",
            Data = response
        };
    }
    // GET ACTIVE PRESCRIPTIONS BY PATIENT ID
    public ApiResponse<List<GetActivePrescriptionsDTO>> GetActivePrescriptionsByPatientId(int patientId)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var prescriptions = _context.Prescriptions
            .Where(p => p.MedicalRecord.Patient.Id == patientId &&
                        p.Status == PRESCRIPTION_STATUS.ACTIVE &&
                        p.ExpiryDate >= today)
            .ToList();

        if (!prescriptions.Any())
        {
            return new ApiResponse<List<GetActivePrescriptionsDTO>>
            {
                Status = 404,
                Message = "No active prescriptions found for this patient.",
                Data = new List<GetActivePrescriptionsDTO>()
            };
        }
        // MAP TO DTO
        var mappedPrescriptions = prescriptions
            .Select(p => _mapper.Map<GetActivePrescriptionsDTO>(p))
            .ToList();

        return new ApiResponse<List<GetActivePrescriptionsDTO>>
        {
            Status = 200,
            Message = "Active prescriptions retrieved successfully.",
            Data = mappedPrescriptions
        };
    }
}