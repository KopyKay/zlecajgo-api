namespace ZlecajGo.Domain.Entities;

public class OfferContractor
{
    public Offer Offer { get; set; } = null!;
    public Guid OfferId { get; set; }
    
    public User Contractor { get; set; } = null!;
    public string ContractorId { get; set; } = null!;
    
    public Status Status { get; set; } = null!;
    public int StatusId { get; set; }
}