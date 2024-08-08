using Microsoft.EntityFrameworkCore;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;
using ZlecajGo.Infrastructure.Persistence;
using Type = ZlecajGo.Domain.Entities.Type;

namespace ZlecajGo.Infrastructure.Repositories;

internal class OfferRepository(ZlecajGoContext dbContext) : IOfferRepository
{
    public async Task<IEnumerable<Offer>> GetOffersAsync()
    {
        var offers = await dbContext.Offers
            .AsNoTracking()
            .Select(o => new Offer
            {
                Id = o.Id,
                Title = o.Title,
                Description = o.Description,
                Price = o.Price,
                PostDateTime = o.PostDateTime,
                ExpiryDateTime = o.ExpiryDateTime,
                Images = o.Images,
                Location = o.Location,
                Type = new Type { Name = o.Type.Name },
                Category = new Category { Name = o.Category.Name },
                Status = new Status { Name = o.Status.Name },
                Provider = new User { FullName = o.Provider.FullName }
            })
            .ToListAsync();

        return offers;
    }

    public async Task<Offer?> GetOfferByIdAsync(Guid offerId)
    {
        var offer = await dbContext.Offers
            .AsNoTracking()
            .Where(o => o.Id == offerId)
            .Select(o => new Offer
            {
                Id = o.Id,
                Title = o.Title,
                Description = o.Description,
                Price = o.Price,
                PostDateTime = o.PostDateTime,
                ExpiryDateTime = o.ExpiryDateTime,
                Images = o.Images,
                Location = o.Location,
                Type = new Type { Name = o.Type.Name },
                Category = new Category { Name = o.Category.Name },
                Status = new Status { Name = o.Status.Name },
                Provider = new User { FullName = o.Provider.FullName }
            })
            .FirstOrDefaultAsync();

        return offer;
    }

    public async Task<Guid> CreateOfferAsync(Offer entity)
    {
        await dbContext.Offers.AddAsync(entity);
        await SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteOfferAsync(Offer entity)
    {
        dbContext.Offers.Remove(entity);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}