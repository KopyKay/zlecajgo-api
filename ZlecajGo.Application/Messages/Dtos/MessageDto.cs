namespace ZlecajGo.Application.Messages.Dtos;

public class MessageDto
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public string SenderId { get; set; } = null!;
    public string MessageText { get; set; } = null!;
    public DateTime SentAt { get; set; }
    public bool IsRead { get; set; } = false;
}