using MediCore.DTOs.AppointmentsDTOs;
using MediCore.Models;
using AutoMapper;
using MediCore.Enums;

namespace MediCore.Profiles
{
    public class AppointmentsMappingProfile : Profile
    {
        public AppointmentsMappingProfile()
        {
            CreateMap<Appointment, AddAppointmentResponseDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType.ToString()));

            CreateMap<Appointment, GetAppointmentsDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType.ToString()));

            CreateMap<Appointment, GetAppointmentsByIdDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType.ToString()));

            CreateMap<UpdateAppointmentDTO, Appointment>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<APPOINTMENT_STATUS>(src.Status)))
                .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => Enum.Parse<APPOINTMENT_TYPE>(src.VisitType)))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType.ToString()));

            CreateMap<Doctor, DoctorBasicDTO>();
            CreateMap<Patient, PatientBasicDTO>();
            CreateMap<Department, DepartmentBasicDTO>();


            CreateMap<Appointment, GetDoctorsAppointmentsDTO>()
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
               .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType))
               .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => new DoctorInfoDTO
               {
                   Id = src.Doctor.Id,
                   Specialty = src.Doctor.Specialty,
                   WorkingHours = src.Doctor.WorkingHours,
                   ExperienceYears = src.Doctor.ExperienceYears
               }));

            CreateMap<Appointment, GetPatientsAppointmentsDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType))
                .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => new PatientInfoDTO
                {
                    Id = src.Patient.Id,
                    PersonalNumber = src.Patient.PersonalNumber,
                    PhoneNumber = src.Patient.PhoneNumber
                }));
        }
    }
}
