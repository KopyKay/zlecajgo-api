using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Users.Commands.UpdateUser;
using ZlecajGo.Application.Users.Dtos;
using ZlecajGo.Application.Users.Queries.GetUser;
using ZlecajGo.Application.Users.Queries.GetUsers;
using ZlecajGo.Domain.Constants;

namespace ZlecajGo.API.Controllers;

[ApiController]
[Route("api/users")]
[Authorize(Policy = PolicyNames.HasProfileCompleted)]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDto>))]
    public async Task<IActionResult> GetUsers()
    {
        var users = await mediator.Send(new GetUsersQuery());
        return Ok(users);
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser([FromRoute] string userId)
    {
        var user = await mediator.Send(new GetUserQuery(userId));
        return Ok(user);
    }
    
    [HttpPatch("update")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}