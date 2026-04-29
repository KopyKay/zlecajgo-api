using ZlecajGo.Domain.Entities;
using ZlecajGo.Infrastructure.Persistence;
using ZlecajGo.Infrastructure.Seeders;
using OfferType = ZlecajGo.Domain.Entities.Type;

namespace ZlecajGo.API.IntegrationTests.Fixtures;

internal class TestDictionarySeeder(ZlecajGoContext dbContext) : IZlecajGoSeeder
{
    public async Task SeedAsync(bool isProduction)
    {
        await dbContext.Database.EnsureCreatedAsync();

        if (!dbContext.Categories.Any())
        {
            dbContext.Categories.AddRange(
                new Category { Id = 1, Name = "Category-1" },
                new Category { Id = 2, Name = "Category-2" });
        }

        if (!dbContext.Statuses.Any())
        {
            dbContext.Statuses.AddRange(
                new Status { Id = 1, Name = "Oczekujace" },
                new Status { Id = 2, Name = "Zajete" });
        }

        if (!dbContext.Types.Any())
        {
            dbContext.Types.AddRange(
                new OfferType { Id = 1, Name = "Type-1" },
                new OfferType { Id = 2, Name = "Type-2" });
        }

        await dbContext.SaveChangesAsync();
    }
}