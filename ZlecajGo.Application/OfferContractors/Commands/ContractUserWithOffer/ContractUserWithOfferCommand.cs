using MediatR;

namespace ZlecajGo.Application.OfferContractors.Commands.ContractUserWithOffer;

public record ContractUserWithOfferCommand(Guid OfferId) : IRequest<bool>
{
    public string? ContractorId { get; set; }
}