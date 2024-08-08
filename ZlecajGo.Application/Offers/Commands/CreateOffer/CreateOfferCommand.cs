using MediatR;

namespace ZlecajGo.Application.Offers.Commands.CreateOffer;

public class CreateOfferCommand : IRequest<Guid>
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTime? ExpiryDateTime { get; set; }
    public byte[][]? Images { get; set; }
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int TypeId { get; set; }
    public int CategoryId { get; set; }
    public int StatusId { get; set; }
    public string ProviderId { get; set; } = null!;
}