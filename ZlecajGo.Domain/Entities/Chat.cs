using System.ComponentModel.DataAnnotations.Schema;

namespace ZlecajGo.Domain.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public string User1Id { get; set; } = null!;
    public string User2Id { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastMessageAt { get; set; }
    
    [InverseProperty(nameof(User.ChatsAsUser1))]
    public User User1 { get; set; } = null!;
    
    [InverseProperty(nameof(User.ChatsAsUser2))]
    public User User2 { get; set; } = null!;
    
    [InverseProperty(nameof(Message.Chat))]
    public ICollection<Message> Messages { get; set; } = [];
}