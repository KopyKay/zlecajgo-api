using Microsoft.EntityFrameworkCore;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;
using ZlecajGo.Infrastructure.Persistence;

namespace ZlecajGo.Infrastructure.Repositories;

internal class OfferRepository(ZlecajGoContext dbContext) : IOfferRepository
{
    public async Task<IEnumerable<Offer>> GetOffersAsync()
    {
        var offers = await dbContext.Offers
            .AsNoTracking()
            .ToListAsync();

        return offers;
    }

    public async Task<Offer?> GetOfferByIdAsync(Guid offerId)
    {
        var offer = await dbContext.Offers
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == offerId);

        return offer;
    }

    public async Task<Offer?> GetOfferByIdWithTrackingAsync(Guid offerId)
    {
        var offer = await dbContext.Offers
            .FirstOrDefaultAsync(o => o.Id == offerId);

        return offer;
    }

    public async Task<Guid> CreateOfferAsync(Offer entity)
    {
        await dbContext.Offers.AddAsync(entity);
        await SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> DeleteOfferAsync(Offer entity)
    {
        if (await HasOfferBeenPerformedAsync(entity.Id)) 
            return false;
        
        dbContext.Offers.Remove(entity);
        await SaveChangesAsync();

        return true;
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    
    private async Task<bool> HasOfferBeenPerformedAsync(Guid offerId)
    {
        var result = await dbContext.OfferContractors
            .AsNoTracking()
            .AnyAsync(co => co.Offer.Id == offerId);

        return result;
    }
}