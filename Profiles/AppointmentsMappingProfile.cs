using MediCore.DTOs.AppointmentsDTOs;
using MediCore.Models;
using AutoMapper;

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

            CreateMap<Doctor, DoctorBasicDTO>();
            CreateMap<Patient, PatientBasicDTO>();
            CreateMap<Department, DepartmentBasicDTO>();
        }
    }
}
