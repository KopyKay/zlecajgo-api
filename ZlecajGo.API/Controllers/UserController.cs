using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Users;
using ZlecajGo.Application.Users.Commands.ChangeUserPassword;
using ZlecajGo.Application.Users.Commands.ConfirmPassword;
using ZlecajGo.Application.Users.Commands.UpdateUser;
using ZlecajGo.Application.Users.Dtos;
using ZlecajGo.Application.Users.Queries.GetCurrentUser;
using ZlecajGo.Application.Users.Queries.GetUser;
using ZlecajGo.Application.Users.Queries.GetUsers;
using ZlecajGo.Domain.Constants;
using ZlecajGo.Domain.Entities;

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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
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

    [HttpPost("confirmPassword")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<IActionResult> ConfirmUserPassword([FromBody] CheckUserPasswordCommand command)
    {
        var isPasswordCorrect = await mediator.Send(command);
        return Ok(isPasswordCorrect);
    }
    
    [HttpPost("changePassword")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordCommand command)
    {
        var passwordChanged = await mediator.Send(command);
        return Ok(passwordChanged);
    }
}