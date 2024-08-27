using MediatR;
using ZlecajGo.Application.Offers.Dtos;

namespace ZlecajGo.Application.Offers.Commands.UpdateOfferStatus;

public class UpdateOfferCommand : IRequest<bool>
{
    public Guid OfferId { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public byte[][]? Images { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public int? StatusId { get; set; }
}