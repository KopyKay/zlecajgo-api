using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Categories.Dtos;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler
(
    ILogger<GetCategoriesQueryHandler> logger,
    ICategoryRepository categoryRepository,
    IMapper mapper
)    
: IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
{
    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all categories");

        var categories = await categoryRepository.GetCategoriesAsync();
        var categoriesDto = mapper.Map<IEnumerable<CategoryDto>>(categories);

        return categoriesDto;
    }
}