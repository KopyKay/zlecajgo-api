using MediatR;
using OneOf;
using ZlecajGo.Application.Categories.Dtos;

namespace ZlecajGo.Application.Categories.Queries.GetCategoryOrCategories;

public record GetCategoryOrCategoriesQuery(int? CategoryId) : IRequest<OneOf<CategoryDto, IEnumerable<CategoryDto>>>;