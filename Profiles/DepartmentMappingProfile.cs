﻿using AutoMapper;
using MediCore.DTOs.DepartmentsDTOs;
using MediCore.Models;

namespace MediCore.Profiles
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<Department, DepartmentAllDTO>()
                .ForMember(dest => dest.DepartmentType, opt =>
                    opt.MapFrom(src => src.DepartmentType)); 

            CreateMap<DepartmentAddDTO, Department>();
            CreateMap<DepartmentUpdateDTO, Department>();
        }
    }
}
