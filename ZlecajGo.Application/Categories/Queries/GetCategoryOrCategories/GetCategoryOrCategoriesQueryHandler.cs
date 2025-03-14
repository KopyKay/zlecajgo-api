using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using ZlecajGo.Application.Categories.Dtos;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Categories.Queries.GetCategoryOrCategories;

public class GetCategoryOrCategoriesQueryHandler
(
    ILogger<GetCategoryOrCategoriesQueryHandler> logger,
    ICategoryRepository categoryRepository,
    IMapper mapper
)    
: IRequestHandler<GetCategoryOrCategoriesQuery, OneOf<CategoryDto, IEnumerable<CategoryDto>>>
{
    public async Task<OneOf<CategoryDto, IEnumerable<CategoryDto>>> Handle(GetCategoryOrCategoriesQuery request, CancellationToken cancellationToken)
    {
        if (request.CategoryId.HasValue)
        {
            var categoryId = request.CategoryId.Value;
            
            logger.LogInformation("Getting category with id [{CategoryId}]", categoryId);

            var category = await categoryRepository.GetCategoryByIdAsync(categoryId)
                           ?? throw new NotFoundException(nameof(Category), categoryId.ToString());
        
            var categoryDto = mapper.Map<CategoryDto>(category);

            return OneOf<CategoryDto, IEnumerable<CategoryDto>>.FromT0(categoryDto);
        }
        
        logger.LogInformation("Getting all categories");

        var categories = await categoryRepository.GetCategoriesAsync();
        var categoriesDto = mapper.Map<IEnumerable<CategoryDto>>(categories);

        return OneOf<CategoryDto, IEnumerable<CategoryDto>>.FromT1(categoriesDto);
    }
}