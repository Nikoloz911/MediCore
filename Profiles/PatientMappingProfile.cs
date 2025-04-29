using MediCore.DTOs.PatientDTOs;
using MediCore.Models;
using AutoMapper;
using MediCore.Enums;
public class PatientMappingProfile : Profile
{
    public PatientMappingProfile()
    {
        // Create Patient from AddDTO
        CreateMap<PatientAddDTO, Patient>()
            .ForMember(dest => dest.GENDER, opt => opt.MapFrom(src => Enum.Parse<GENDER>(src.Gender, true)))
            .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => Enum.Parse<BLOOD_TYPE>(src.BloodType, true)))
            .ForMember(dest => dest.UserId, opt => opt.Ignore());

        // Create DTO from Patient for Created response
        CreateMap<Patient, PatientCreatedDTO>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.GENDER.ToString()))
            .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.BloodType.ToString()))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.User.Status));

        // 🔥 NEW: Add mappings for your GET APIs

        // For PatientGetDTO (simple list version)
        CreateMap<Patient, PatientGetDTO>();

        // For PatientGetByIdDTO (detailed view)
        CreateMap<Patient, PatientGetByIdDTO>()
            .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src => src.Appointments))
            .ForMember(dest => dest.MedicalRecords, opt => opt.MapFrom(src => src.MedicalRecords));
    }
}
