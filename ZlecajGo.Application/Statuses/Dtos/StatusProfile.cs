using AutoMapper;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Statuses.Dtos;

public class StatusProfile : Profile
{
    public StatusProfile()
    {
        CreateMap<Status, StatusDto>();
    }
}