using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Chats.Dtos;
using ZlecajGo.Application.Chats.Queries.GetCurrentUserChatOrChats;
using ZlecajGo.Domain.Constants;

namespace ZlecajGo.API.Controllers;

[ApiController]
[Route("api/chats")]
[Authorize(Policy = PolicyNames.HasProfileCompleted)]
public class ChatController(IMediator mediator) : ControllerBase
{
    [HttpGet("currentUserChatOrChats")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChatDto))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChatDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentUserChatOrChats([FromQuery] Guid? chatId)
    {
        var result = await mediator.Send(new GetCurrentUserChatOrChatsQuery(chatId));
        return result.Match<IActionResult>(Ok, Ok);
    }
}