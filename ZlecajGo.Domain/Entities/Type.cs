namespace ZlecajGo.Domain.Entities;

public class Type
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    public List<Offer> Offers { get; set; } = [];
}