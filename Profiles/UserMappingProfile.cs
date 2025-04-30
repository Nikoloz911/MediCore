using MediCore.DTOs.UserDTOs;
using AutoMapper;
using MediCore.Models;
using MediCore.Enums;
using MediCore.Request;
namespace MediCore.Profiles;
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserGetDTO>();
        CreateMap<User, UserGetByIdDTO>();
        CreateMap<UserUpdateDTO, User>();
        CreateMap<AddUserDTO, User>();
        CreateMap<AddUserDTO, AddUser>();
        CreateMap<AddUser, AddUserDTO>();
        CreateMap<User, PublicUserDTO>();

    }
}
