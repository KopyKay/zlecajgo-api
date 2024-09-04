namespace ZlecajGo.Application.OfferContractors.Dtos;

public class OfferContractorDto
{
    public Guid OfferId { get; set; }
    public string ContractorId { get; set; } = null!;
    public int StatusId { get; set; }
}