using AutoMapper;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Users.Dtos;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}