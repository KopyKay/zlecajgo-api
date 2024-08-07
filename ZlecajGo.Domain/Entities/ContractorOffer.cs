namespace ZlecajGo.Domain.Entities;

public class ContractorOffer
{
    public User Contractor { get; set; } = null!;
    public string ContractorId { get; set; } = null!;
    
    public Offer Offer { get; set; } = null!;
    public Guid OfferId { get; set; }
    
    public Status Status { get; set; } = null!;
    public int StatusId { get; set; }
}