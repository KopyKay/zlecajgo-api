using ZlecajGo.Domain.Entities;
using OfferType = ZlecajGo.Domain.Entities.Type;

namespace ZlecajGo.Application.UnitTests.Helpers.TestData;

public static class OfferTestData
{
    public static Offer Create(Guid? id = null, string? providerId = null) => new()
    {
        Id = id ?? Guid.NewGuid(),
        Title = "Test offer",
        Description = "Test description",
        Price = 100m,
        PostDateTime = DateTime.UtcNow.AddDays(-1),
        ExpiryDateTime = DateTime.UtcNow.AddDays(3),
        Location = new Location
        {
            City = "City",
            Street = "Street",
            ZipCode = "12-345",
            Latitude = 52.0,
            Longitude = 21.0
        },
        Category = new Category { Id = 1, Name = "Category" },
        CategoryId = 1,
        Status = new Status { Id = 1, Name = "Status" },
        StatusId = 1,
        Type = new OfferType { Id = 1, Name = "Type" },
        TypeId = 1,
        ProviderId = providerId ?? "provider-1"
    };
}