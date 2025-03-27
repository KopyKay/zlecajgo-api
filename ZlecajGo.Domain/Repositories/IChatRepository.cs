using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Domain.Repositories;

public interface IChatRepository
{
    Task <Chat?> GetChatByIdAsync(Guid chatId);
    Task<Chat?> GetUserChatAsync(string userId, Guid chatId);
    Task<IEnumerable<Chat>> GetUserChatsAsync(string userId);
    Task<Guid> CreateChatAsync(Chat chat);
    Task<Guid> CreateMessageAsync(Message message);
    Task UpdateChatAsync(Chat chat);
    Task UpdateMessageAsync(Message message);
}