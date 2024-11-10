using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Identity.Queries.IsEmailExist;
using ZlecajGo.Application.Identity.Queries.IsPhoneNumberExist;
using ZlecajGo.Application.Identity.Queries.IsUserNameExist;

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
    
    [HttpGet("isPhoneNumberExists")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<IActionResult> IsPhoneNumberExists([FromQuery] string phoneNumber)
    {
        var isPhoneNumberExists = await mediator.Send(new IsPhoneNumberExistQuery(phoneNumber));
        return Ok(isPhoneNumberExists);
    }
    
    [HttpGet("isUserNameExists")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<IActionResult> IsUserNameExists([FromQuery] string userName)
    {
        var isUserNameExists = await mediator.Send(new IsUserNameExistQuery(userName));
        return Ok(isUserNameExists);
    }
}