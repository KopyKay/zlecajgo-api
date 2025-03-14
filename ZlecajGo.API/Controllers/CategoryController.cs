using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Categories.Dtos;
using ZlecajGo.Application.Categories.Queries.GetCategoryOrCategories;
using ZlecajGo.Domain.Constants;

namespace ZlecajGo.API.Controllers;

[ApiController]
[Route("api/categories")]
[Authorize(Policy = PolicyNames.HasProfileCompleted)]
public class CategoryController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryOrCategories([FromQuery] int? categoryId)
    {
        var result = await mediator.Send(new GetCategoryOrCategoriesQuery(categoryId));
        return result.Match<IActionResult>(Ok, Ok);
    }
}