using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Categories.Dtos;
using ZlecajGo.Application.Categories.Queries.GetCategories;

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
}