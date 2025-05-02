using AutoMapper;
using MediCore.DTOs.MedicalRecordsDTOs;
using MediCore.Models;

namespace MediCore.Profiles;
public class MedicalRecordsMappingProfile : Profile
{
    public MedicalRecordsMappingProfile()
    {
        CreateMap<MedicalRecord, GetPatientsMedicalRecordsDTO>();
        CreateMap<Patient, PatientBasicInfo>();
        CreateMap<MedicalRecord, GetMedicalRecordsDTO>();

        CreateMap<UpdateMedicalRecordDTO, MedicalRecord>()
            .ForMember(dest => dest.Complaints, opt => opt.MapFrom(src => src.Complaints))
            .ForMember(dest => dest.Symptoms, opt => opt.MapFrom(src => src.Symptoms))
            .ForMember(dest => dest.Measurements, opt => opt.MapFrom(src => src.Measurements));

        CreateMap<CreateMedicalRecordDTO, MedicalRecord>();
        CreateMap<MedicalRecord, MedicalRecordResponseDTO>();
    }
}
