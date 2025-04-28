using AutoMapper;
using MediCore.Data;
using Microsoft.AspNetCore.Mvc;
using MediCore.Core;
using MediCore.DTOs.DoctorDTOs;
using MediCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediCore.Enums;

namespace MediCore.Services.Implementations
{
    public class DoctorService : IDoctor
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<DoctorUpdateDTO> _doctorValidator;
        public DoctorService(DataContext context, IMapper mapper, IValidator<DoctorUpdateDTO> doctorValidator)
        {
            _context = context;
            _mapper = mapper;
            _doctorValidator = doctorValidator;
        }

        // GET ALL DOCTORS
        public ApiResponse<List<DoctorAllDTO>> GetAllDoctors()
        {
            var doctors = _context.Doctors.Include(d => d.User).ToList(); 
            if (!doctors.Any())
            {
                return new ApiResponse<List<DoctorAllDTO>>
                {
                    Status = 404,
                    Message = "No doctors found.",
                    Data = null
                };
            }
            var doctorDtos = _mapper.Map<List<DoctorAllDTO>>(doctors);
            return new ApiResponse<List<DoctorAllDTO>>
            {
                Status = 200,
                Message = "Doctors retrieved successfully.",
                Data = doctorDtos
            };
        }
        // GET DOCTOR BY ID
        public ApiResponse<DoctorByIdDTO> GetDoctorById(int id)
        {
            var doctor = _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Department)
                .FirstOrDefault(d => d.UserId == id);
            if (doctor == null)
            {
                return new ApiResponse<DoctorByIdDTO>
                {
                    Status = 404,
                    Message = "Doctor not found.",
                    Data = null
                };
            }
            var doctorDto = _mapper.Map<DoctorByIdDTO>(doctor);
            return new ApiResponse<DoctorByIdDTO>
            {
                Status = 200,
                Message = "Doctor retrieved successfully.",
                Data = doctorDto
            };
        }
        // GET DOCTOR BY DEPARTMENTS ID
        public ApiResponse<List<DoctorsByDepartmentDTO>> GetDoctorsByDepartment(int departmentId)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == departmentId);
            if (department == null)
            {
                return new ApiResponse<List<DoctorsByDepartmentDTO>>
                {
                    Status = 404,
                    Message = "Department not found.",
                    Data = null
                };
            }
            var doctors = _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Department)
                .Where(d => d.DepartmentId == departmentId)
                .ToList();
            if (doctors.Count == 0)
            {
                return new ApiResponse<List<DoctorsByDepartmentDTO>>
                {
                    Status = 404,
                    Message = "No doctors found in this department.",
                    Data = new List<DoctorsByDepartmentDTO>()
                };
            }
            // MAP DOCTORS TO DOCTORS BY DEPARTMENT DTO
            var doctorDTOs = _mapper.Map<List<DoctorsByDepartmentDTO>>(doctors);
            return new ApiResponse<List<DoctorsByDepartmentDTO>>
            {
                Status = 200,
                Message = "Doctors retrieved successfully.",
                Data = doctorDTOs
            };
        }

        // GET DOCTOR SCHEDULE BY DOCTOR ID
        public ApiResponse<DoctorScheduleDTO> GetDoctorSchedule(int doctorId)
        {
            var doctor = _context.Doctors
                .Include(d => d.User)
                .FirstOrDefault(d => d.UserId == doctorId);
            if (doctor == null)
            {
                return new ApiResponse<DoctorScheduleDTO>
                {
                    Status = 404,
                    Message = "Doctor not found.",
                    Data = null
                };
            }
            // MAP DOCTOR TO DOCTOR SCHEDULE DTO
            var scheduleDto = _mapper.Map<DoctorScheduleDTO>(doctor);
            return new ApiResponse<DoctorScheduleDTO>
            {
                Status = 200,
                Message = "Doctor schedule retrieved successfully.",
                Data = scheduleDto
            };
        }
        // UPDATE DOCTOR BY ID
            public ApiResponse<DoctorByIdDTO> UpdateDoctor(int id, DoctorUpdateDTO doctorUpdateDTO)
            {
            // VALIDATE WITH FLUENT VALIDATOR
            var validationResult = _doctorValidator.Validate(doctorUpdateDTO);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new ApiResponse<DoctorByIdDTO>
                {
                    Status = 400,
                    Message = string.Join(" | ", errorMessages), 
                    Data = null
                };
            }
            // VALIDATE SPECIALTY
            var departmentId = ValidateSpecialty(doctorUpdateDTO.Specialty);
                if (departmentId == -1)
                {
                    return new ApiResponse<DoctorByIdDTO>
                    {
                        Status = 400,
                        Message = $"Invalid specialty: {doctorUpdateDTO.Specialty}",
                        Data = null
                    };
                }
                var doctor = _context.Doctors
                    .Include(d => d.User)
                    .Include(d => d.Department)
                    .FirstOrDefault(d => d.UserId == id);
                if (doctor == null)
                {
                    return new ApiResponse<DoctorByIdDTO>
                    {
                        Status = 404,
                        Message = "Doctor not found.",
                        Data = null
                    };
                }
            // Update Doctor properties
            // MAP DOCTOR UPDATE DTO TO DOCTOR
            _mapper.Map(doctorUpdateDTO, doctor);
                _mapper.Map(doctorUpdateDTO, doctor.User);
                if (!string.IsNullOrEmpty(doctorUpdateDTO.Password))
                {
                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(doctorUpdateDTO.Password);
                    doctor.User.Password = hashedPassword;
                }
                _context.SaveChanges();
                var doctorDto = _mapper.Map<DoctorByIdDTO>(doctor);

                return new ApiResponse<DoctorByIdDTO>
                {
                    Status = 200,
                    Message = "Doctor updated successfully.",
                    Data = doctorDto
                };
            }

          // VALIDATION METHOD FOR SPECIALTY
        private int ValidateSpecialty(string specialty)
        {
           switch (specialty.ToLower())
           {
                    case "cardiology": return 1;
                    case "neurology": return 2;
                    case "orthopedics": return 3;
                    case "pediatrics": return 4;
                    case "dermatology": return 5;
                    case "psychiatry": return 6;
                    case "gastroenterology": return 7;
                    case "radiology": return 8;
                    default:
                    return -1; 
           }
        }
    }
}
