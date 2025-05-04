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
        CreateMap<Prescription, GetPatientPrescriptionsDTO>();

        CreateMap<AddPrescriptionsDTO, Prescription>()
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<PRESCRIPTION_STATUS>(src.Status, true)));

        CreateMap<Prescription, AddPrescriptionsResponseDTO>();

        CreateMap<UpdatePrescriptionDTO, Prescription>()
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<PRESCRIPTION_STATUS>(src.Status, true)));

        CreateMap<Prescription, UpdatePrescriptionResponseDTO>();
        CreateMap<Prescription, GetActivePrescriptionsDTO>();
        CreateMap<AddPrescriptionItemDTO, PrescriptionItem>();
        CreateMap<PrescriptionItem, AddPrescriptionItemResponseDTO>();
    }
}
