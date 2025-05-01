using AutoMapper;
using FluentValidation;
using MediCore.Core;
using MediCore.Data;
using MediCore.DTOs.AppointmentsDTOs;
using MediCore.Enums;
using MediCore.Models;
using MediCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace MediCore.Services.Implementations;
public class AppointmentsService : IAppointments
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public AppointmentsService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET APPOINTMENTS BY FILTERS
    public ApiResponse<List<GetAppointmentsDTO>> GetAppointments(
        int? doctorId = null,
        int? patientId = null,
        int? departmentId = null,
        APPOINTMENT_STATUS? status = null,
        APPOINTMENT_TYPE? visitType = null,
        DateTime? date = null
    )
    {
        var query = _context.Appointments.AsQueryable();

        if (doctorId.HasValue)
        {
            query = query.Where(a => a.DoctorId == doctorId.Value);
        }

        if (patientId.HasValue)
        {
            query = query.Where(a => a.PatientId == patientId.Value);
        }

        if (departmentId.HasValue)
        {
            query = query.Where(a => a.DepartmentId == departmentId.Value);
        }

        if (status.HasValue)
        {
            query = query.Where(a => a.Status == status.Value);
        }

        if (visitType.HasValue)
        {
            query = query.Where(a => a.VisitType == visitType.Value);
        }

        if (date.HasValue)
        {
            query = query.Where(a => a.Date.Date == date.Value.Date);
        }

        var appointments = query.ToList();

        if (!appointments.Any())
        {
            return new ApiResponse<List<GetAppointmentsDTO>>
            {
                Status = 404,
                Message = "No appointments found matching the filters.",
                Data = new List<GetAppointmentsDTO>()
            };
        }
        // MAP APPOINTMENTS TO DTO
        var mapped = _mapper.Map<List<GetAppointmentsDTO>>(appointments);

        return new ApiResponse<List<GetAppointmentsDTO>>
        {
            Status = 200,
            Message = "Appointments retrieved successfully.",
            Data = mapped
        };
    }

    // GET APPOINTMENT BY ID
    public ApiResponse<GetAppointmentsByIdDTO> GetAppointmentById(int id)
    {
        var appointment = _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.Department)
            .FirstOrDefault(a => a.Id == id);

        if (appointment == null)
        {
            return new ApiResponse<GetAppointmentsByIdDTO>
            {
                Status = 404,
                Message = "Appointment not found.",
                Data = null
            };
        }
        // MAP APPOINTMENT TO DTO
        var mapped = _mapper.Map<GetAppointmentsByIdDTO>(appointment);

        return new ApiResponse<GetAppointmentsByIdDTO>
        {
            Status = 200,
            Message = "Appointment retrieved successfully.",
            Data = mapped
        };
    }
    // ADD NEW APPOINTMENT
    public ApiResponse<AddAppointmentResponseDTO> AddAppointment(AddAppointmentDTO dto)
    {
        // Validation for null DTO
        if (dto == null)
        {
            return new ApiResponse<AddAppointmentResponseDTO>
            {
                Status = 400,
                Message = "DTO is null",
                Data = null
            };
        }
        // VALIDATE DATE TIME
        if (dto.Date < DateTime.Now)
        {
            return new ApiResponse<AddAppointmentResponseDTO>
            {
                Status = 400,
                Message = "Appointment date cannot be in the past.",
                Data = null
            };
        }
        // VALIDATE DOCTOR
        var doctor = _context.Doctors.FirstOrDefault(d => d.Id == dto.DoctorId);
        if (doctor == null)
        {
            return new ApiResponse<AddAppointmentResponseDTO>
            {
                Status = 400,
                Message = "Doctor not found.",
                Data = null
            };
        }
        // VALIDATE PATIENT
        var patient = _context.Patients.FirstOrDefault(p => p.Id == dto.PatientId);
        if (patient == null)
        {
            return new ApiResponse<AddAppointmentResponseDTO>
            {
                Status = 400,
                Message = "Patient not found.",
                Data = null
            };
        }
        // VALIDATE DEPARTMENT
        var department = _context.Departments.FirstOrDefault(d => d.Id == dto.DepartmentId);
        if (department == null)
        {
            return new ApiResponse<AddAppointmentResponseDTO>
            {
                Status = 400,
                Message = "Department not found.",
                Data = null
            };
        }

        // Validate Status enum
        if (!Enum.TryParse(dto.Status, true, out APPOINTMENT_STATUS statusEnum))
        {
            return new ApiResponse<AddAppointmentResponseDTO>
            {
                Status = 400,
                Message = "Invalid Status value. Valid Values:  Primary, FollowUp, Emergency",
                Data = null
            };
        }

        // Validate VisitType enum
        if (!Enum.TryParse(dto.VisitType, true, out APPOINTMENT_TYPE visitTypeEnum))
        {
            return new ApiResponse<AddAppointmentResponseDTO>
            {
                Status = 400,
                Message = "Invalid VisitType value. Valid Values: Scheduled, Completed, Cancelled",
                Data = null
            };
        }

        // CREATE APPOINTMENT
        var appointment = new Appointment
        {
            Date = dto.Date,
            Time = dto.Time,
            Duration = dto.Duration,
            DoctorId = dto.DoctorId,
            PatientId = dto.PatientId,
            DepartmentId = dto.DepartmentId,
            Status = statusEnum,
            VisitType = visitTypeEnum
        };

        _context.Appointments.Add(appointment);
        _context.SaveChanges();

        // Map to response DTO
        var responseDto = _mapper.Map<AddAppointmentResponseDTO>(appointment);

        return new ApiResponse<AddAppointmentResponseDTO>
        {
            Status = 200,
            Message = "Appointment created successfully.",
            Data = responseDto
        };
    }

    // UPDATE APPOINTMENT BY ID
    public ApiResponse<UpdateAppointmentDTO> UpdateAppointment(int id, UpdateAppointmentDTO updateDto)
    {
        // VALIDATE IF APPOINTMENT EXISTS
        var appointment = _context.Appointments.FirstOrDefault(a => a.Id == id);
        if (appointment == null)
        {
            return new ApiResponse<UpdateAppointmentDTO>
            {
                Status = 404,
                Message = "Appointment not found",
                Data = null
            };
        }
        // VALIDATE DOCTOR
        var doctor = _context.Doctors.FirstOrDefault(d => d.Id == updateDto.DoctorId);
        if (doctor == null)
        {
            return new ApiResponse<UpdateAppointmentDTO>
            {
                Status = 400,
                Message = "Doctor not found",
                Data = null
            };
        }
        // VALIDATE DEPARTMENT
        var department = _context.Departments.FirstOrDefault(d => d.Id == updateDto.DepartmentId);
        if (department == null)
        {
            return new ApiResponse<UpdateAppointmentDTO>
            {
                Status = 400,
                Message = "Department not found",
                Data = null
            };
        }
        // VALIDATE APPOINTMENT STATUS
        if (!Enum.TryParse(updateDto.Status, true, out APPOINTMENT_STATUS statusEnum))
        {
            return new ApiResponse<UpdateAppointmentDTO>
            {
                Status = 400,
                Message = "Invalid appointment status",
                Data = null
            };
        }
        // VALIDATE VISIT TYPE
        if (!Enum.TryParse(updateDto.VisitType, true, out APPOINTMENT_TYPE visitTypeEnum))
        {
            return new ApiResponse<UpdateAppointmentDTO>
            {
                Status = 400,
                Message = "Invalid visit type",
                Data = null
            };
        }
        // VALIDATE DATE TIME
        if (updateDto.Date < DateTime.Now.Date)
        {
            return new ApiResponse<UpdateAppointmentDTO>
            {
                Status = 400,
                Message = "Appointment date cannot be in the past.",
                Data = null
            };
        }
        // UPDATE APPOINTMENT PROPERTIES
        appointment.Date = updateDto.Date;
        appointment.Time = updateDto.Time;
        appointment.Duration = updateDto.Duration;
        appointment.DoctorId = updateDto.DoctorId;
        appointment.DepartmentId = updateDto.DepartmentId;
        appointment.Status = statusEnum;
        appointment.VisitType = visitTypeEnum;
        _context.SaveChanges();

        // MAP APPOINTMENT TO DTO
        var updatedDto = _mapper.Map<UpdateAppointmentDTO>(appointment);

        // Return successful response
        return new ApiResponse<UpdateAppointmentDTO>
        {
            Status = 200,
            Message = "Appointment updated successfully.",
            Data = updatedDto
        };
    }
    // DELETE APPOINTMENT BY ID
    public ApiResponse<string> DeleteAppointment(int id)
    {
        var appointment = _context.Appointments.FirstOrDefault(a => a.Id == id);
        if (appointment == null)
        {
            return new ApiResponse<string>
            {
                Status = 404,
                Message = "Appointment not found",
                Data = null
            };
        }
        // REMOVE APPOINTMENT
        _context.Appointments.Remove(appointment);
        _context.SaveChanges();
        return new ApiResponse<string>
        {
            Status = 200,
            Message = "Appointment deleted successfully",
            Data = null
        };
    }
    // GET DOCTOR APPOINTMENTS BY DOCTOR ID
    public ApiResponse<List<GetDoctorsAppointmentsDTO>> GetAppointmentsByDoctorId(int doctorId)
    {
        var appointments = _context.Appointments
            .Where(a => a.DoctorId == doctorId)
            .Include(a => a.Doctor)
            .ToList();

        if (appointments.Count == 0)
        {
            return new ApiResponse<List<GetDoctorsAppointmentsDTO>>
            {
                Status = 404,
                Message = "No appointments found for this doctor.",
                Data = null
            };
        }
        // MAP APPPOINTMENTS TO DTO
        var appointmentDtos = _mapper.Map<List<GetDoctorsAppointmentsDTO>>(appointments);
        return new ApiResponse<List<GetDoctorsAppointmentsDTO>>
        {
            Status = 200,
            Message = "Appointments retrieved successfully.",
            Data = appointmentDtos
        };
    }
    // GET PATIENT APPOINTMENTS BY PATIENT ID
    public ApiResponse<List<GetPatientsAppointmentsDTO>> GetAppointmentsByPatientId(int patientId)
    {
        var appointments = _context.Appointments
            .Where(a => a.PatientId == patientId)
            .Include(a => a.Patient)
            .ToList();

        if (appointments.Count == 0)
        {
            return new ApiResponse<List<GetPatientsAppointmentsDTO>>
            {
                Status = 404,
                Message = "No appointments found for this patient.",
                Data = null
            };
        }
        // MAP APPOINTMENTS TO DTO
        var appointmentDtos = _mapper.Map<List<GetPatientsAppointmentsDTO>>(appointments);
        return new ApiResponse<List<GetPatientsAppointmentsDTO>>
        {
            Status = 200,
            Message = "Appointments retrieved successfully.",
            Data = appointmentDtos
        };
    }
}