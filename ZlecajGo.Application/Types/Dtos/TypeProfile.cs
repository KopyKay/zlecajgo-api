using AutoMapper;
using Type = ZlecajGo.Domain.Entities.Type;

namespace ZlecajGo.Application.Types.Dtos;

public class TypeProfile : Profile
{
    public TypeProfile()
    {
        CreateMap<Type, TypeDto>();
    }
}