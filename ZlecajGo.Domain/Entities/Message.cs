using System.ComponentModel.DataAnnotations.Schema;

namespace ZlecajGo.Domain.Entities;

public class Message
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public string SenderId { get; set; } = null!;
    public string MessageText { get; set; } = null!;
    public DateTime SentAt { get; set; }
    public bool IsRead { get; set; } = false;

    [InverseProperty(nameof(Entities.Chat.Messages))]
    public Chat Chat { get; set; } = null!;
    
    [InverseProperty(nameof(User.MessagesSent))]
    public User Sender { get; set; } = null!;
}