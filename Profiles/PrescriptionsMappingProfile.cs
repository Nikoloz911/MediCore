using AutoMapper;
using MediCore.DTOs.PrescriptionsDTOs;
using MediCore.Enums;
using MediCore.Models;
namespace MediCore.Profiles;
public class PrescriptionsMappingProfile : Profile
{
    public PrescriptionsMappingProfile()
    {
        CreateMap<Prescription, GetPrescriptionsByIdDTO>();
        CreateMap<Prescription, GetPatientPrescriptionsDTO>()
            .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.MedicalRecord.Patient));

        CreateMap<Patient, PrescriptinosPatientBasicInfo>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.GENDER, opt => opt.MapFrom(src => src.GENDER))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.InsuranceDetails, opt => opt.MapFrom(src => src.InsuranceDetails))
            .ForMember(dest => dest.Allergies, opt => opt.MapFrom(src => src.Allergies))
            .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.BloodType));

        CreateMap<User, PrescriptinosPatientBasicInfo>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Patient.DateOfBirth))
            .ForMember(dest => dest.GENDER, opt => opt.MapFrom(src => src.Patient.GENDER))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Patient.PhoneNumber))
            .ForMember(dest => dest.InsuranceDetails, opt => opt.MapFrom(src => src.Patient.InsuranceDetails))
            .ForMember(dest => dest.Allergies, opt => opt.MapFrom(src => src.Patient.Allergies))
            .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => src.Patient.BloodType));

        CreateMap<AddPrescriptionsDTO, Prescription>()
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<PRESCRIPTION_STATUS>(src.Status, true)));

        CreateMap<Prescription, AddPrescriptionsResponseDTO>();

        CreateMap<UpdatePrescriptionDTO, Prescription>()
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<PRESCRIPTION_STATUS>(src.Status, true)));

        CreateMap<Prescription, UpdatePrescriptionResponseDTO>();
        CreateMap<Prescription, GetActivePrescriptionsDTO>();
    }
}
