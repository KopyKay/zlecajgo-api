using AutoMapper;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Chats.Dtos;

public class ChatProfile : Profile
{
    public ChatProfile()
    {
        CreateMap<Chat, ChatDto>();
        CreateMap<ChatDto, Chat>();
    }
}