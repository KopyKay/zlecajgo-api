using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Types.Dtos;
using ZlecajGo.Application.Types.Queries.GetType;
using ZlecajGo.Application.Types.Queries.GetTypes;

namespace ZlecajGo.API.Controllers;

[ApiController]
[Route("api/types")]
[Authorize]
public class TypeController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeDto>))]
    public async Task<IActionResult> GetTypes()
    {
        var types = await mediator.Send(new GetTypesQuery());
        return Ok(types);
    }
    
    [HttpGet("{typeId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetType([FromRoute] int typeId)
    {
        var type = await mediator.Send(new GetTypeQuery(typeId));
        return Ok(type);
    }
}