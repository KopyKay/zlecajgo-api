using AutoMapper;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Categories.Dtos;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
    }
}