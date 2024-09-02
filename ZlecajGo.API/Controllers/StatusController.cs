using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Statuses.Dtos;
using ZlecajGo.Application.Statuses.Queries.GetStatus;
using ZlecajGo.Application.Statuses.Queries.GetStatuses;
using ZlecajGo.Infrastructure.Authorization;

namespace ZlecajGo.API.Controllers;

[ApiController]
[Route("api/statuses")]
[Authorize]
[Authorize(Policy = PolicyNames.HasProfileCompleted)]
public class StatusController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StatusDto>))]
    public async Task<IActionResult> GetStatuses()
    {
        var statuses = await mediator.Send(new GetStatusesQuery());
        return Ok(statuses);
    }
    
    [HttpGet("{statusId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StatusDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStatus([FromRoute] int statusId)
    {
        var status = await mediator.Send(new GetStatusQuery(statusId));
        return Ok(status);
    }
}