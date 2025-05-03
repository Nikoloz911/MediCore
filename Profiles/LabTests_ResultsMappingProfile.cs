using AutoMapper;
using MediCore.DTOs.LabTests_ResultsDTOs;
using MediCore.Models;
namespace MediCore.Profiles;
public class LabTests_ResultsMappingProfile : Profile
{
    public LabTests_ResultsMappingProfile()
    {
        CreateMap<LabTest, GetLabTestsDTO>();
        CreateMap<AddLabTestsDTO, LabTest>();
        CreateMap<LabTest, AddLabTestsResponseDTO>();     
        CreateMap<AddLabResultDTO, LabResult>(); 
        CreateMap<LabResult, AddLabResultsResponseDTO>();
        CreateMap<LabResult, GetPatientLabResults>()
          .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient)); 
        CreateMap<Patient, PatientLabResultsBasicInfo>();
    }
}