using System.Text.Json.Serialization;
using MediatR;

namespace ZlecajGo.Application.OfferContractors.Commands.UpdateContractedOffer;

public class UpdateContractedOfferCommand: IRequest
{
    [JsonIgnore]
    public Guid OfferId { get; set; }
    [JsonIgnore]
    public string? ContractorId { get; set; }
    public int StatusId { get; set; }
}