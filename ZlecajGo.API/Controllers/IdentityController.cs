using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Identity.Queries.IsEmailExist;

namespace ZlecajGo.API.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpGet("isEmailExists")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<IActionResult> IsEmailExists([FromQuery] string email)
    {
        var isEmailExists = await mediator.Send(new IsEmailExistQuery(email));
        return Ok(isEmailExists);
    }
}