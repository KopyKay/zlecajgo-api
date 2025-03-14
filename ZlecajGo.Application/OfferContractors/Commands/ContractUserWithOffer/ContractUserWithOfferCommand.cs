using MediatR;

namespace ZlecajGo.Application.OfferContractors.Commands.ContractUserWithOffer;

public class ContractUserWithOfferCommand : IRequest<bool>
{
    public Guid OfferId { get; set; }
    public string ContractorId { get; set; } = null!;
    public int? StatusId { get; set; }
}