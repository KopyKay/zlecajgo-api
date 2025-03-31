using ZlecajGo.Application.Messages.Dtos;

namespace ZlecajGo.Application.Chats.Dtos;

public class ChatDto
{
    public Guid Id { get; set; }
    public string User1Id { get; set; } = null!;
    public string User2Id { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastMessageAt { get; set; }
    public List<MessageDto> Messages { get; set; } = [];
}