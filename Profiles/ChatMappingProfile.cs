using AutoMapper;
using MediCore.DTOs.ChatDTOs;
using MediCore.Models;

namespace MediCore.Profiles;

public class ChatMappingProfile : Profile
{
    public ChatMappingProfile()
    {
        CreateMap<ChatMessage, ChatMessageSendDTO>();
        CreateMap<ChatMessage, ChatMessageSendDTO>();
    }
}
