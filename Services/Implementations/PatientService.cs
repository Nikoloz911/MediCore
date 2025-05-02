using AutoMapper;
using MediCore.Core;
using MediCore.Data;
using MediCore.DTOs.PatientDTOs;
using MediCore.Models;
using MediCore.Services.Interfaces;
using MediCore.Enums;
using MediCore.Validators;
using BCrypt.Net;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Services.Implementations;

public class PatientService : IPatient
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<PatientAddDTO> _validator;
    public PatientService(DataContext context, IMapper mapper, IValidator<PatientAddDTO> validator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
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


    // ADD NEW PATIENT
    public ApiResponse<PatientCreatedDTO> AddPatient(PatientAddDTO dto)
    {
        // VALIDATE DTO
        var validationResult = _validator.Validate(dto);

        if (!validationResult.IsValid)
        {
            string errorMessage = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new ApiResponse<PatientCreatedDTO>
            {
                Status = StatusCodes.Status400BadRequest,
                Message = errorMessage,
                Data = null
            };
        }

        // CHECK FOR EXISTING USER
        var existingUser = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
        if (existingUser != null)
        {
            return new ApiResponse<PatientCreatedDTO>
            {
                Status = StatusCodes.Status409Conflict,
                Message = "User with this email already exists.",
                Data = null
            };
        }

        // CHECK FOR EXISTING PERSONAL NUMBER
        var existingPatient = _context.Patients.FirstOrDefault(p => p.PersonalNumber == dto.PersonalNumber);
        if (existingPatient != null)
        {
            return new ApiResponse<PatientCreatedDTO>
            {
                Status = StatusCodes.Status409Conflict,
                Message = "Patient with this Personal Number already exists.",
                Data = null
            };
        }

        // PARSE ENUMS
        if (!Enum.TryParse(dto.Gender, true, out GENDER gender) ||
            !Enum.TryParse(dto.BloodType, true, out BLOOD_TYPE bloodType))
        {
            return new ApiResponse<PatientCreatedDTO>
            {
                Status = StatusCodes.Status400BadRequest,
                Message = "Invalid gender or blood type value.",
                Data = null
            };
        }

        // CREATE USER
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Password = hashedPassword,
            Role = USER_ROLE.PATIENT,
            Status = USER_STATUS.ACTIVE
        };
        _context.Users.Add(user);
        _context.SaveChanges();
        // CREATE PATIENT
        var patient = _mapper.Map<Patient>(dto);
        patient.UserId = user.Id;
        patient.GENDER = gender;
        patient.BloodType = bloodType;
        _context.Patients.Add(patient);
        _context.SaveChanges();

        // MAP TO DTO RESPONSE
        var createdDto = _mapper.Map<PatientCreatedDTO>((user, patient));

        return new ApiResponse<PatientCreatedDTO>
        {
            Status = StatusCodes.Status200OK,
            Message = "Patient added successfully.",
            Data = createdDto
        };
    }

    // GET PATIENT HISTORY BY ID
    public ApiResponse<PatientHistoryDTO> GetPatientHistory(int patientId)
    {
        var patient = _context.Patients
            .Where(p => p.Id == patientId)
            .Include(p => p.User)
            .Include(p => p.MedicalRecords)
            .FirstOrDefault();

        if (patient == null)
        {
            return new ApiResponse<PatientHistoryDTO>
            {
                Status = StatusCodes.Status404NotFound,
                Message = "Patient not found.",
                Data = null
            };
        }
        var patientHistoryDto = _mapper.Map<PatientHistoryDTO>(patient);
        return new ApiResponse<PatientHistoryDTO>
        {
            Status = StatusCodes.Status200OK,
            Message = "Medical history retrieved successfully.",
            Data = patientHistoryDto
        };
    }
}