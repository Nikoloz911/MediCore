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
               .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType));

            CreateMap<Appointment, GetPatientsAppointmentsDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.VisitType, opt => opt.MapFrom(src => src.VisitType));
        }
    }
}
