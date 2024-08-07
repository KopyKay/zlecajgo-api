using Microsoft.AspNetCore.Identity;

namespace ZlecajGo.Domain.Entities;

public class User : IdentityUser
{
    public string FullName { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public bool IsActive { get; set; } = true;
    
    public List<Offer> ProvidedOffers { get; set; } = [];
    public List<Offer> ContractedOffers { get; set; } = [];
    public List<Review> ReviewsGiven { get; set; } = [];
    public List<Review> ReviewsReceived { get; set; } = [];
}