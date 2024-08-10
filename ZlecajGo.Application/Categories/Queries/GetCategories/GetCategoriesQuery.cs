using MediatR;
using ZlecajGo.Application.Categories.Dtos;

namespace ZlecajGo.Application.Categories.Queries.GetCategories;

public class GetCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;