using ZlecajGo.Domain.Entities;
using Type = ZlecajGo.Domain.Entities.Type;

namespace ZlecajGo.Application.Offers.Dtos;

public class OfferDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTime PostDateTime { get; set; }
    public DateTime ExpiryDateTime { get; set; }
    public byte[][]? Images { get; set; }
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string CategoryName { get; set; } = null!;
    public string StatusName { get; set; } = null!;
    public string TypeName { get; set; } = null!;
    public string ProviderFullName { get; set; } = null!;
}