using System.Text.Json.Serialization;
using MediatR;

namespace ZlecajGo.Application.OfferContractors.Commands.UpdateContractedOffer;

public class UpdateContractedOfferCommand : IRequest
{
    public Guid OfferId { get; set; }
    public string ContractorId { get; set; } = null!;
    public DateTime StartDateTime { get; set; }
    public int StatusId { get; set; }
}