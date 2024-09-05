using Microsoft.EntityFrameworkCore;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;
using ZlecajGo.Infrastructure.Persistence;

namespace ZlecajGo.Infrastructure.Repositories;

internal class OfferContractorRepository(ZlecajGoContext dbContext) : IOfferContractorRepository
{
    public async Task<IEnumerable<OfferContractor>> GetContractedOffersAsync()
    {
        var contractedOffers = await dbContext.OfferContractors
            .AsNoTracking()
            .ToListAsync();

        return contractedOffers;
    }

    public async Task<OfferContractor?> GetContractedOfferByIdAsync(Guid offerId, string contractorId)
    {
        var contractedOffer = await dbContext.OfferContractors
            .AsNoTracking()
            .FirstOrDefaultAsync(oc => oc.OfferId == offerId && oc.ContractorId == contractorId);

        return contractedOffer;
    }

    public async Task<OfferContractor?> GetContractedOfferByIdWithTrackingAsync(Guid offerId, string contractorId)
    {
        var offerContractor = await dbContext.OfferContractors
            .FirstOrDefaultAsync(oc => oc.OfferId == offerId && oc.ContractorId == contractorId);

        return offerContractor;
    }

    public async Task ContractUserToOfferAsync(OfferContractor entity)
    {
        await dbContext.OfferContractors.AddAsync(entity);
        await SaveChangesAsync();
    }

    public async Task<bool> CheckIsContractedOfferOccupiedAsync(Guid offerId)
    {
        var isOccupied = await dbContext.OfferContractors
            .AsNoTracking()
            .AnyAsync(oc => oc.OfferId == offerId && oc.StatusId != 6);

        return isOccupied;
    }
    
    public async Task<bool> HasOfferBeenPerformedAsync(Guid offerId)
    {
        var result = await dbContext.OfferContractors
            .AsNoTracking()
            .AnyAsync(co => co.Offer.Id == offerId);

        return result;
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}