using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Statuses.Dtos;
using ZlecajGo.Application.Statuses.Queries.GetStatusOrStatuses;
using ZlecajGo.Domain.Constants;

namespace ZlecajGo.API.Controllers;

[ApiController]
[Route("api/statuses")]
[Authorize(Policy = PolicyNames.HasProfileCompleted)]
public class StatusController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StatusDto))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StatusDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStatusOrStatuses([FromQuery] int? statusId)
    {
        var result = await mediator.Send(new GetStatusOrStatusesQuery(statusId));
        return result.Match<IActionResult>(Ok, Ok);
    }
}