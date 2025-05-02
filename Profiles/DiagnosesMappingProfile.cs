using AutoMapper;
using MediCore.DTOs.DiagnosesDTOs;
using MediCore.Models;
using MediCore.Services.Interfaces;

namespace MediCore.Profiles;
public class DiagnosesMappingProfile : Profile
{
    public DiagnosesMappingProfile()
    {
        CreateMap<Diagnoses, GetPatientDiagnosesDTO>()
            .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.MedicalRecord.Patient));

        CreateMap<Diagnoses, GetMedicalRecordsDiagnosesDTO>()
            .ForMember(dest => dest.MedicalRecord, opt => opt.MapFrom(src => src.MedicalRecord));

        CreateMap<MedicalRecord, MedicalRecordBasicInfo>();
        CreateMap<Patient, DiagnosesPatientBasicInfo>();

        CreateMap<AddDiagnosesDTO, Diagnoses>()
              .ForMember(dest => dest.ICD10Code, opt => opt.MapFrom(src => src.ICD10Code))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.AdditionalComments, opt => opt.MapFrom(src => src.AdditionalComments));
        CreateMap<Diagnoses, AddDiagnosesResponseDTO>();
        CreateMap<UpdateDiagnosesDTO, Diagnoses>();
        CreateMap<Diagnoses, UpdateDiagnosesResponseDTO>();

    }
}
