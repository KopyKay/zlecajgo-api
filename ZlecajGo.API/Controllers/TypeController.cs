using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Types.Dtos;
using ZlecajGo.Application.Types.Queries.GetTypeOrTypes;
using ZlecajGo.Domain.Constants;

namespace ZlecajGo.API.Controllers;

[ApiController]
[Route("api/types")]
[Authorize(Policy = PolicyNames.HasProfileCompleted)]
public class TypeController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeDto))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTypeOrTypes([FromQuery] int? typeId)
    {
        var result = await mediator.Send(new GetTypeOrTypesQuery(typeId));
        return result.Match<IActionResult>(Ok, Ok);
    }
}