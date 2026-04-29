using ZlecajGo.Application.Offers.Commands.CreateOffer;

namespace ZlecajGo.Application.UnitTests.Helpers.TestData;

public static class OfferCommandTestData
{
    public static CreateOfferCommand CreateValidCreateOfferCommand() => new()
    {
        Title = "Valid title",
        Description = "Valid description",
        Price = 120.50m,
        ExpiryDateTime = DateTime.UtcNow.AddDays(3),
        City = "City",
        Street = "Street",
        ZipCode = "12-345",
        Latitude = 52.0,
        Longitude = 21.0,
        TypeId = 1,
        CategoryId = 1
    };
}