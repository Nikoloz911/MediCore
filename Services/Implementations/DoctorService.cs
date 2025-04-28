using AutoMapper;
using MediCore.Data;
using Microsoft.AspNetCore.Mvc;
using MediCore.Core;
using MediCore.DTOs.DoctorDTOs;
using MediCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

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

            var scheduleDto = new DoctorScheduleDTO
            {
                DoctorId = doctor.UserId,
                DoctorName = $"{doctor.User.FirstName}",
                WorkingHours = doctor.WorkingHours
            };

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
            var validationResult = _doctorValidator.Validate(doctorUpdateDTO);

            if (!validationResult.IsValid)
            {
                string validationMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));

                return new ApiResponse<DoctorByIdDTO>
                {
                    Status = 400,
                    Message = $"Validation failed: {validationMessages}", 
                    Data = null
                };
            }
            var doctor = _context.Doctors
                .Include(d => d.User)
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
            doctor.User.FirstName = doctorUpdateDTO.FirstName;
            doctor.User.LastName = doctorUpdateDTO.LastName;
            doctor.User.Email = doctorUpdateDTO.Email;
            doctor.Specialty = doctorUpdateDTO.Specialty;
            doctor.LicenseNumber = doctorUpdateDTO.LicenseNumber;
            doctor.WorkingHours = doctorUpdateDTO.WorkingHours;
            doctor.ExperienceYears = doctorUpdateDTO.ExperienceYears;
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





    }
}
