using Microsoft.EntityFrameworkCore;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;
using ZlecajGo.Infrastructure.Persistence;

namespace ZlecajGo.Infrastructure.Repositories;

internal class ChatRepository(ZlecajGoContext dbContext) : IChatRepository
{
    public async Task<Chat?> GetUserChatAsync(string userId, Guid chatId)
    {
        var chat = await dbContext.Chats
            .AsNoTracking()
            .Include(c => c.Messages.OrderByDescending(m => m.SentAt))
                .ThenInclude(m => m.Sender)
            .Include(c => c.User1)
            .Include(c => c.User2)
            .FirstOrDefaultAsync(c => (c.User1.Id == userId || c.User2.Id == userId) && c.Id == chatId);

        return chat;
    }

    public async Task<IEnumerable<Chat>> GetUserChatsAsync(string userId)
    {
        var chats = await dbContext.Chats
            .AsNoTracking()
            .Include(c => c.Messages.OrderByDescending(m => m.SentAt))
                .ThenInclude(m => m.Sender)
            .Include(c => c.User1)
            .Include(c => c.User2)
            .Where(c => c.User1.Id == userId || c.User2.Id == userId)
            .OrderByDescending(c => c.LastMessageAt)
            .ToListAsync();

        return chats;
    }

    public async Task<Guid> CreateChatAsync(Chat chat)
    {
        await dbContext.Chats.AddAsync(chat);
        await SaveChangesAsync();

        return chat.Id;
    }

    public async Task<Guid> CreateMessageAsync(Message message)
    {
        await dbContext.Messages.AddAsync(message);
        await SaveChangesAsync();
        
        return message.Id;
    }

    public async Task UpdateChatLastMessageAtAsync(Guid chatId, DateTime lastMessageAt)
    {
        await dbContext.Chats
            .Where(c => c.Id == chatId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.LastMessageAt, lastMessageAt));
    }

    public async Task UpdateMessageIsReadAsync(Message message)
    {
        await dbContext.Messages
            .Where(m => m.Id == message.Id && m.ChatId == message.ChatId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(m => m.IsRead, true));
    }

    public async Task UpdateMessagesIsReadAsync(Guid chatId, IEnumerable<Guid> messageIds)
    {
        await dbContext.Messages
            .Where(m => messageIds.Contains(m.Id) && m.ChatId == chatId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(m => m.IsRead, true));
    }
    
    private async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}