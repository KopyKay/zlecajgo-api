using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Users;
using ZlecajGo.Application.Users.Commands.UpdateUser;
using ZlecajGo.Application.Users.Dtos;
using ZlecajGo.Application.Users.Queries.ConfirmPassword;
using ZlecajGo.Application.Users.Queries.GetCurrentUser;
using ZlecajGo.Application.Users.Queries.GetUser;
using ZlecajGo.Application.Users.Queries.GetUsers;
using ZlecajGo.Domain.Constants;

namespace ZlecajGo.API.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = PolicyNames.HasProfileCompleted)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDto>))]
    public async Task<IActionResult> GetUsers()
    {
        var users = await mediator.Send(new GetUsersQuery());
        return Ok(users);
    }

    [HttpGet("{userId}")]
    [Authorize(Policy = PolicyNames.HasProfileCompleted)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser([FromRoute] string userId)
    {
        var user = await mediator.Send(new GetUserQuery(userId));
        return Ok(user);
    }
    
    [HttpGet("currentUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CurrentUser))]
    public async Task<IActionResult> GetCurrentUser()
    {
        var user = await mediator.Send(new GetCurrentUserQuery());
        return Ok(user);
    }
    
    [HttpPatch("update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpGet("confirmPassword")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<IActionResult> ConfirmUserPassword([FromQuery] string password)
    {
        var isPasswordCorrect = await mediator.Send(new CheckUserPasswordQuery(password));
        return Ok(isPasswordCorrect);
    }
}