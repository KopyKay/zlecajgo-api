using System.Text.Json.Serialization;
using MediatR;

namespace ZlecajGo.Application.Offers.Commands.UpdateOfferStatus;

public class UpdateOfferStatusCommand : IRequest
{
    [JsonIgnore]
    public Guid OfferId { get; set; }
    public int StatusId { get; set; }
}