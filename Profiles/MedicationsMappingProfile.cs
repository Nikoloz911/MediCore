using AutoMapper;
using MediCore.DTOs.MedicationsDTOs;
using MediCore.Models;
namespace MediCore.Profiles;
public class MedicationsMappingProfile : Profile
{
    public MedicationsMappingProfile() 
    {
        CreateMap<Medication, GetAllMedications>();
        CreateMap<Medication, GetMedication>();
        CreateMap<AddMedicationDTO, Medication>();
        CreateMap<Medication, AddMedicationResponseDTO>();
        CreateMap<UpdateMedicationDTO, Medication>();
    }
}
