using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ZlecajGo.Domain.Entities;

public class User : IdentityUser
{
    public string? FullName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public bool IsProfileCompleted { get; set; } = false;
    public bool IsActive { get; set; } = true;
    
    public List<Offer> ProvidedOffers { get; set; } = [];
    public List<Offer> ContractedOffers { get; set; } = [];
    public List<Review> ReviewsGiven { get; set; } = [];
    public List<Review> ReviewsReceived { get; set; } = [];
    
    [InverseProperty(nameof(Chat.User1))]
    public ICollection<Chat> ChatsAsUser1 { get; set; } = [];
    
    [InverseProperty(nameof(Chat.User2))]
    public ICollection<Chat> ChatsAsUser2 { get; set; } = [];
    
    [InverseProperty(nameof(Message.Sender))]
    public ICollection<Message> MessagesSent { get; set; } = [];
}