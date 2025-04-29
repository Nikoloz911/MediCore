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

namespace MediCore.Services.Implementations;

public class PatientService : IPatient
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator _validator;
    public PatientService(DataContext context, IMapper mapper, IValidator validator)
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
        // VALIDATE EMAIL
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
        // VALIDATE PERSONAL NUMBER
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

        GENDER gender;
        BLOOD_TYPE bloodType;
        try
        {
            gender = Enum.Parse<GENDER>(dto.Gender, true);
            bloodType = Enum.Parse<BLOOD_TYPE>(dto.BloodType, true);
        }
        catch
        {
            return new ApiResponse<PatientCreatedDTO>
            {
                Status = StatusCodes.Status400BadRequest,
                Message = "Invalid gender or blood type value.",
                Data = null
            };
        }
        // VALIDATE DATE OF BIRTH
        if (dto.DateOfBirth > DateTime.Now)
        {
            return new ApiResponse<PatientCreatedDTO>
            {
                Status = StatusCodes.Status400BadRequest,
                Message = "Date of birth cannot be in the future.",
                Data = null
            };
        }
        // VALIDATE DATE OF BIRTH
        if (dto.DateOfBirth != dto.DateOfBirth.Date) 
        {
            return new ApiResponse<PatientCreatedDTO>
            {
                Status = StatusCodes.Status400BadRequest,
                Message = "Invalid date of birth.",
                Data = null
            };
        }


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
        // MAP DTO TO PATIENT
        var patient = _mapper.Map<Patient>(dto);
        patient.UserId = user.Id;
        patient.GENDER = gender;
        patient.BloodType = bloodType;
        _context.Patients.Add(patient);
        _context.SaveChanges();

        var createdDto = new PatientCreatedDTO
        {
            Id = patient.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PersonalNumber = patient.PersonalNumber,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.GENDER.ToString(),  
            ContactInfo = patient.ContactInfo,
            InsuranceDetails = patient.InsuranceDetails,
            Allergies = patient.Allergies,
            BloodType = patient.BloodType.ToString(), 
            Role = user.Role,  
            Status = user.Status  
        };


        return new ApiResponse<PatientCreatedDTO>
        {
            Status = StatusCodes.Status200OK,
            Message = "Patient added successfully.",
            Data = createdDto
        };
    }







}