using AutoMapper;
using MediCore.DTOs.DoctorDTOs;
using MediCore.Models;  

namespace MediCore.Profiles
{
    public class DoctorMappingProfile : Profile
    {
        public DoctorMappingProfile()
        {

            CreateMap<DoctorUpdateDTO, Doctor>()
               .ForMember(dest => dest.LicenseNumber, opt => opt.MapFrom(src => src.LicenseNumber))
               .ForMember(dest => dest.WorkingHours, opt => opt.MapFrom(src => src.WorkingHours))
               .ForMember(dest => dest.ExperienceYears, opt => opt.MapFrom(src => src.ExperienceYears));

            CreateMap<DoctorUpdateDTO, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<Doctor, DoctorAllDTO>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty));

            CreateMap<Doctor, DoctorByIdDTO>()
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
               .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty))
               .ForMember(dest => dest.LicenseNumber, opt => opt.MapFrom(src => src.LicenseNumber))
               .ForMember(dest => dest.WorkingHours, opt => opt.MapFrom(src => src.WorkingHours))
               .ForMember(dest => dest.ExperienceYears, opt => opt.MapFrom(src => src.ExperienceYears))
               .ForMember(dest => dest.DepartmentType, opt => opt.MapFrom(src => src.Department.DepartmentType.ToString()));

            CreateMap<Doctor, DoctorsByDepartmentDTO>()
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty))
                .ForMember(dest => dest.LicenseNumber, opt => opt.MapFrom(src => src.LicenseNumber))
                .ForMember(dest => dest.WorkingHours, opt => opt.MapFrom(src => src.WorkingHours))
                .ForMember(dest => dest.ExperienceYears, opt => opt.MapFrom(src => src.ExperienceYears))
                 .ForMember(dest => dest.DepartmentType, opt => opt.MapFrom(src => src.Department.DepartmentType.ToString()));

            CreateMap<Doctor, DoctorScheduleDTO>()
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.WorkingHours, opt => opt.MapFrom(src => src.WorkingHours));
        }
    }
}