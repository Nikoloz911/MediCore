using MediCore.DTOs.UserDTOs;
using AutoMapper;
using MediCore.Models;
namespace MediCore.Profiles;
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserGetDTO>();

        CreateMap<User, UserGetByIdDTO>();

        CreateMap<UserUpdateDTO, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
