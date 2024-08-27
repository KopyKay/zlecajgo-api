using MediatR;
using ZlecajGo.Application.Categories.Dtos;

namespace ZlecajGo.Application.Categories.Queries.GetCategory;

public record GetCategoryQuery(int CategoryId) : IRequest<CategoryDto>;