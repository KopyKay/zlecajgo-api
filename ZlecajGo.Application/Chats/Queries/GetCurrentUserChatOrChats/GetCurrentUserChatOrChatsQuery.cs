using MediatR;
using OneOf;
using ZlecajGo.Application.Chats.Dtos;

namespace ZlecajGo.Application.Chats.Queries.GetCurrentUserChatOrChats;

public record GetCurrentUserChatOrChatsQuery(Guid? ChatId) : IRequest<OneOf<ChatDto, IEnumerable<ChatDto>>>;