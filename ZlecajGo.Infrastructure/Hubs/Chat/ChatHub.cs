using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Chats.Dtos;
using ZlecajGo.Application.Messages.Dtos;
using ZlecajGo.Domain.Constants;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Infrastructure.Hubs.Chat;

[Authorize(Policy = PolicyNames.HasProfileCompleted)]
public sealed class ChatHub
(
    ILogger<ChatHub> logger, 
    IChatRepository chatRepository,
    IUserStore<User> userStore,
    IMapper mapper
)
: Hub<IChatHub>
{
    private static readonly Dictionary<string, HashSet<string>> UserConnectionMap = new();
    
    public override Task OnConnectedAsync()
    {
        var userId = GetUserId();
        var connectionId = GetConnectionId();
        
        lock (UserConnectionMap)
        {
            if (!UserConnectionMap.TryGetValue(userId, out var connections))
            {
                connections = [];
                UserConnectionMap[userId] = connections;
            }
            
            connections.Add(connectionId);
        }
        
        logger.LogInformation("User {UserId} connected to chat hub with connection ID {ConnectionId}",
                              userId, connectionId);
        
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = GetUserId();

        var connectionId = GetConnectionId();
        
        lock (UserConnectionMap)
        {
            if (UserConnectionMap.TryGetValue(userId, out var connections))
            {
                connections.Remove(connectionId);
                
                if (connections.Count == 0)
                {
                    UserConnectionMap.Remove(userId);
                }
            }
        }
        
        logger.LogInformation("User {UserId} disconnected from chat hub", userId);
        
        return base.OnDisconnectedAsync(exception);
    }

    public async Task CreateChat(string recipientId, string initialMessage = "")
    {
        var userId = GetUserId();
        
        _ = await userStore.FindByIdAsync(recipientId, CancellationToken.None)
            ?? throw new NotFoundException(nameof(User), recipientId);

        var chat = (await chatRepository.GetUserChatsAsync(userId))
                   .FirstOrDefault(c => c.User1Id == recipientId || c.User2Id == recipientId);
        
        if (chat is null)
        {
            string user1Id, user2Id;
            if (string.CompareOrdinal(userId, recipientId) < 0)
            {
                user1Id = userId;
                user2Id = recipientId;
            }
            else
            {
                user1Id = recipientId;
                user2Id = userId;
            }

            chat = new Domain.Entities.Chat { User1Id = user1Id, User2Id = user2Id };
            chat.Id = await chatRepository.CreateChatAsync(chat);
        }

        if (!string.IsNullOrEmpty(initialMessage))
        {
            await SendMessage(chat.Id, initialMessage);
        }

        var chatDto = mapper.Map<ChatDto>(chat);

        await SendToUserAsync(recipientId, client => client.ReceiveChat(chatDto), true);
    }
    
    public async Task SendMessage(Guid chatId, string content)
    {
        var userId = GetUserId();
        var chat = await GetUserChatAsync(userId, chatId);
        var message = await CreateChatMessageAsync(userId, chatId, content);
        var messageDto = mapper.Map<MessageDto>(message);
        var recipientId = GetChatPartnerId(chat!, userId);

        await SendToUserAsync(recipientId, client => client.ReceiveMessage(messageDto), true);
    }

    public async Task ReadMessages(Guid chatId)
    {
        var userId = GetUserId();
        var chat = await GetUserChatAsync(userId, chatId);
        var messages = chat!.Messages.Where(m => !m.IsRead && m.SenderId != userId).ToList();

        if (messages.Count == 0) return;
        
        var messageIds = messages.Select(m => m.Id).ToList();
        var senderId = GetChatPartnerId(chat, userId);

        await chatRepository.UpdateMessagesIsReadAsync(chatId, messageIds);
        
        await SendToUserAsync(senderId, client => client.ReceiveReadMessages(messageIds));
    }

    private string GetUserId()
    {
        return Context.UserIdentifier ?? throw new NullHubContextException();
    }
    private string GetConnectionId()
    {
        return Context.ConnectionId;
    }
    private async Task<Domain.Entities.Chat?> GetUserChatAsync(string userId, Guid chatId)
    {
        return await chatRepository.GetUserChatAsync(userId, chatId)
            ?? throw new NotFoundException(nameof(Chat), $"{chatId} for user [{userId}]");
    }
    private async Task<Message> CreateChatMessageAsync(string userId, Guid chatId, string content)
    {
        var message = new Message
        {
            ChatId = chatId,
            SenderId = userId,
            MessageText = content
        };
        message.Id = await chatRepository.CreateMessageAsync(message);
        await chatRepository.UpdateChatLastMessageAtAsync(chatId, message.SentAt);

        return message;
    }
    private static string GetChatPartnerId(Domain.Entities.Chat chat, string userId)
    {
        return chat.User1Id == userId ? chat.User2Id : chat.User1Id;
    }

    private async Task SendToUserAsync(string userId, Func<IChatHub, Task> sendAction, bool sendToCaller = false)
    {
        HashSet<string>? connectionsCopy = null;
        
        lock (UserConnectionMap)
        {
            if (UserConnectionMap.TryGetValue(userId, out var connections))
            {
                connectionsCopy = new HashSet<string>(connections);
            }
        }

        if (connectionsCopy != null)
        {
            foreach (var client in connectionsCopy.Select(connectionId => Clients.Client(connectionId)))
            {
                await sendAction(client);
            }
        }
        
        if (sendToCaller)
        {
            await sendAction(Clients.Caller);
        }
    }
}