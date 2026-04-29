using ZlecajGo.Application.Offers.Commands.CreateOffer;
using ZlecajGo.Application.Offers.Commands.UpdateOffer;

namespace ZlecajGo.API.IntegrationTests.Helpers.TestData;

public static class OfferRequestTestData
{
    public static CreateOfferCommand CreateValidOffer() => new CreateOfferCommand
    {
        Title = "Test offer",
        Description = "Offer description",
        Price = 120.50m,
        ExpiryDateTime = DateTime.UtcNow.AddDays(2),
        City = "City",
        Street = "Street",
        ZipCode = "12-345",
        Latitude = 52.0,
        Longitude = 21.0,
        TypeId = 1,
        CategoryId = 1
    };

    public static UpdateOfferCommand CreateUpdateOfferRequest() => new UpdateOfferCommand
    {
        Description = "Updated description",
        Price = 150m,
        City = "Updated City",
        Street = "Updated Street",
        ZipCode = "98-765",
        Latitude = 51.0,
        Longitude = 20.0
    };
}