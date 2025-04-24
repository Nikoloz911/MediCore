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
        CreateMap<UserUpdateDTO, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Add mapping for AddUserDTO to User
        CreateMap<AddUserDTO, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore());

        // Add mapping between AddUserDTO and AddUser (both directions)
        CreateMap<AddUserDTO, AddUser>();
        CreateMap<AddUser, AddUserDTO>();
    }
}
