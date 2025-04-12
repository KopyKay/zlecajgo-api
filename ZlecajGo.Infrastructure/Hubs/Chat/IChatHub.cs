using ZlecajGo.Application.Chats.Dtos;
using ZlecajGo.Application.Messages.Dtos;

namespace ZlecajGo.Infrastructure.Hubs.Chat;

public interface IChatHub
{
    Task ReceiveChat(ChatDto chat);
    Task ReceiveMessage(MessageDto message);
    Task ReceiveReadMessages(List<Guid> messageIds);
}