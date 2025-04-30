using MediCore.DTOs.PatientDTOs;
using MediCore.Models;
using AutoMapper;
using MediCore.Enums;
public class PatientMappingProfile : Profile
{
    public PatientMappingProfile()
    {
        CreateMap<PatientAddDTO, Patient>();

        CreateMap<Patient, PatientCreatedDTO>();

        CreateMap<Patient, PatientGetDTO>();

        CreateMap<Patient, PatientGetByIdDTO>();
    }
}
