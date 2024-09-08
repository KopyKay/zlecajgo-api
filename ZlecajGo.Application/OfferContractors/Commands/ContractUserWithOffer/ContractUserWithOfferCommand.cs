using MediatR;

namespace ZlecajGo.Application.OfferContractors.Commands.ContractUserWithOffer;

public record ContractUserWithOfferCommand(string ContractorId, Guid OfferId) : IRequest<bool>
{
    public int? StatusId { get; set; }
}