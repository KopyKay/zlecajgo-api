namespace ZlecajGo.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    public List<Offer> Offers { get; set; } = [];
}