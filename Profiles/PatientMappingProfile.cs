using MediCore.DTOs.PatientDTOs;
using MediCore.Models;
using AutoMapper;
public class PatientMappingProfile : Profile
{
    public PatientMappingProfile()
    {
        CreateMap<PatientAddDTO, Patient>();

        CreateMap<Patient, PatientCreatedDTO>();

        CreateMap<Patient, PatientGetDTO>();

        CreateMap<Patient, PatientGetByIdDTO>();
        CreateMap<(User user, Patient patient), PatientCreatedDTO>()    
              .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.user.FirstName))
              .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.user.LastName))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.user.Email))
              .ForMember(dest => dest.PersonalNumber, opt => opt.MapFrom(src => src.patient.PersonalNumber))
              .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.patient.DateOfBirth))
              .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.patient.GENDER.ToString()))
              .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.patient.ContactInfo))
              .ForMember(dest => dest.InsuranceDetails, opt => opt.MapFrom(src => src.patient.InsuranceDetails))
              .ForMember(dest => dest.Allergies, opt => opt.MapFrom(src => src.patient.Allergies))
              .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.patient.BloodType.ToString()))
              .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.user.Role))
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.user.Status));

        CreateMap<Patient, PatientHistoryDTO>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PersonalNumber, opt => opt.MapFrom(src => src.PersonalNumber))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.GENDER.ToString()))
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo))
                .ForMember(dest => dest.InsuranceDetails, opt => opt.MapFrom(src => src.InsuranceDetails))
                .ForMember(dest => dest.Allergies, opt => opt.MapFrom(src => src.Allergies))
                .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.BloodType.ToString()))
                .ForMember(dest => dest.MedicalRecords, opt => opt.MapFrom(src => src.MedicalRecords));
    }
}
