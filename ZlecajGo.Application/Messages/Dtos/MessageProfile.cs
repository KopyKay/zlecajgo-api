using AutoMapper;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Messages.Dtos;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Message, MessageDto>();
        CreateMap<MessageDto, Message>();
    }
}