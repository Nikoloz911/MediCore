using AutoMapper;
using FluentValidation;
using MediCore.Services.Interfaces;
using MediCore.Data;
using MediCore.Request;
using MediCore.Core;
using MediCore.DTOs.MedicalRecordsDTOs;
using Microsoft.EntityFrameworkCore;
using MediCore.Models;

namespace MediCore.Services.Implementations;
public class MedicalRecordsService : IMedicalRecords
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateMedicalRecordDTO> _validator;
    private readonly IValidator<UpdateMedicalRecordDTO> _updateValidator;
    public MedicalRecordsService(DataContext context, IMapper mapper, IValidator<CreateMedicalRecordDTO> validator, IValidator<UpdateMedicalRecordDTO> updateValidator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
        _updateValidator = updateValidator;
    }


    // GET MEDICAL RECORDS BY PATIENT ID
    public ApiResponse<List<GetPatientsMedicalRecordsDTO>> GetMedicalRecordsByPatientId(int patientId)
    {
        var records = _context.MedicalRecords
            .Where(m => m.PatientId == patientId)
            .Include(m => m.Patient) 
            .ToList();

        if (!records.Any())
        {
            return new ApiResponse<List<GetPatientsMedicalRecordsDTO>>
            {
                Status = 404,
                Message = "No medical records found for the specified patient.",
                Data = new List<GetPatientsMedicalRecordsDTO>()
            };
        }
        // MAP TO DTO
        var dtoList = _mapper.Map<List<GetPatientsMedicalRecordsDTO>>(records);

        return new ApiResponse<List<GetPatientsMedicalRecordsDTO>>
        {
            Status = 200,
            Message = "Medical records retrieved successfully.",
            Data = dtoList
        };
    }
    // GET MEDICAL RECORD BY ID
    public ApiResponse<GetMedicalRecordsDTO> GetMedicalRecordById(int id)
    {
        var record = _context.MedicalRecords.FirstOrDefault(m => m.Id == id);

        if (record == null)
        {
            return new ApiResponse<GetMedicalRecordsDTO>
            {
                Status = 404,
                Message = "Medical record not found.",
                Data = null
            };
        }
        // MAP TO DTO
        var dto = _mapper.Map<GetMedicalRecordsDTO>(record);

        return new ApiResponse<GetMedicalRecordsDTO>
        {
            Status = 200,
            Message = "Medical record retrieved successfully.",
            Data = dto
        };
    }
    // ADD NEW MEDICAL RECORD
    public ApiResponse<MedicalRecordResponseDTO> CreateMedicalRecord(CreateMedicalRecordDTO dto)
    {
        // VALIDATE
        var validationResult = _validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            return new ApiResponse<MedicalRecordResponseDTO>
            {
                Status = 400,
                Message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)),
                Data = null
            };
        }
        // CHECK IF PATIENT EXISTS
        var patient = _context.Patients.FirstOrDefault(p => p.Id == dto.PatientId);
        if (patient == null)
        {
            return new ApiResponse<MedicalRecordResponseDTO>
            {
                Status = 404,
                Message = "Patient not found.",
                Data = null
            };
        }
        // CHECK IF DOCTOR EXISTS
        var doctor = _context.Doctors.FirstOrDefault(d => d.Id == dto.DoctorId);
        if (doctor == null)
        {
            return new ApiResponse<MedicalRecordResponseDTO>
            {
                Status = 404,
                Message = "Doctor not found.",
                Data = null
            };
        }
        // MAP DTO TO ENTITY
        var record = _mapper.Map<MedicalRecord>(dto);
        record.CreatedAt = DateTime.UtcNow;

        _context.MedicalRecords.Add(record);
        _context.SaveChanges();
        // MAP TO DTO
        var responseDto = _mapper.Map<MedicalRecordResponseDTO>(record);
        return new ApiResponse<MedicalRecordResponseDTO>
        {
            Status = 200,
            Message = "Medical record created successfully.",
            Data = responseDto
        };
    }

    // UPDATE MEDICAL RECORD
    public ApiResponse<MedicalRecordResponseDTO> UpdateMedicalRecord(int id, UpdateMedicalRecordDTO dto)
    {
        var validationResult = _updateValidator.Validate(dto);
        if (!validationResult.IsValid)
        {
            return new ApiResponse<MedicalRecordResponseDTO>
            {
                Status = 400,
                Message = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)),
                Data = null
            };
        }
        var record = _context.MedicalRecords.FirstOrDefault(r => r.Id == id);
        if (record == null)
        {
            return new ApiResponse<MedicalRecordResponseDTO>
            {
                Status = 404,
                Message = "Medical record not found.",
                Data = null
            };
        }
        _mapper.Map(dto, record);
        _context.SaveChanges();

        // MAP TO DTO
        var responseDto = _mapper.Map<MedicalRecordResponseDTO>(record);

        return new ApiResponse<MedicalRecordResponseDTO>
        {
            Status = 200,
            Message = "Medical record updated successfully.",
            Data = responseDto
        };
    }
}
