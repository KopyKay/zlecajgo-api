using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Categories.Dtos;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Categories.Queries.GetCategory;

public class GetCategoryQueryHandler 
(
    ILogger<GetCategoryQueryHandler> logger,
    ICategoryRepository categoryRepository,
    IMapper mapper
)    
: IRequestHandler<GetCategoryQuery, CategoryDto>
{
    public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting category with id [{CategoryId}]", request.CategoryId);

        var category = await categoryRepository.GetCategoryByIdAsync(request.CategoryId)
            ?? throw new NotFoundException(nameof(Category), request.CategoryId.ToString());
        
        var categoryDto = mapper.Map<CategoryDto>(category);

        return categoryDto;
    }
}