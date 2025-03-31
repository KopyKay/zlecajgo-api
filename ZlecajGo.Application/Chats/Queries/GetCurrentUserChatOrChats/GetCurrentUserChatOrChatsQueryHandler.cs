using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using ZlecajGo.Application.Chats.Dtos;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Chats.Queries.GetCurrentUserChatOrChats;

public class GetCurrentUserChatOrChatsQueryHandler 
(
    ILogger<GetCurrentUserChatOrChatsQueryHandler> logger,
    IUserContext userContext,
    IChatRepository chatRepository,
    IMapper mapper
) 
: IRequestHandler<GetCurrentUserChatOrChatsQuery, OneOf<ChatDto, IEnumerable<ChatDto>>>
{
    public async Task<OneOf<ChatDto, IEnumerable<ChatDto>>> Handle(GetCurrentUserChatOrChatsQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = userContext.GetCurrentUser()!.Id;
        
        if (request.ChatId.HasValue)
        {
            logger.LogInformation("Getting chat with id [{@ChatId}]", request.ChatId.Value);
            
            var chatId = request.ChatId.Value;
            var chat = await chatRepository.GetUserChatAsync(currentUserId, chatId)
                ?? throw new NotFoundException(nameof(Chat), chatId.ToString());
            
            var chatDto = mapper.Map<ChatDto>(chat);
            
            return OneOf<ChatDto, IEnumerable<ChatDto>>.FromT0(chatDto);
        }
        
        logger.LogInformation("Getting all chats for user [{UserId}]", currentUserId);
        
        var chats = await chatRepository.GetUserChatsAsync(currentUserId);
        var chatsDto = mapper.Map<IEnumerable<ChatDto>>(chats);

        return OneOf<ChatDto, IEnumerable<ChatDto>>.FromT1(chatsDto);
    }
}