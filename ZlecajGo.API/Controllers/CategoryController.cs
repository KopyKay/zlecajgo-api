using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Categories.Dtos;
using ZlecajGo.Application.Categories.Queries.GetCategories;
using ZlecajGo.Application.Categories.Queries.GetCategory;

namespace ZlecajGo.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryDto>))]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await mediator.Send(new GetCategoriesQuery());
        return Ok(categories);
    }
    
    [HttpGet("{categoryId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategory([FromRoute] int categoryId)
    {
        var category = await mediator.Send(new GetCategoryQuery(categoryId));
        return Ok(category);
    }
}