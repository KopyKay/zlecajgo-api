namespace ZlecajGo.Domain.Entities;

public class Offer
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTime PostDateTime { get; set; }
    public DateTime ExpiryDateTime { get; set; }
    public byte[][]? Images { get; set; }
    public Location Location { get; set; } = null!;
    
    public Category Category { get; set; } = null!;
    public int CategoryId { get; set; }
    
    public Status Status { get; set; } = null!;
    public int StatusId { get; set; }
    
    public Type Type { get; set; } = null!;
    public int TypeId { get; set; }
    
    public User Provider { get; set; } = null!;
    public string ProviderId { get; set; } = null!;
    
    public List<User> Contractors { get; set; } = [];
}