using Microsoft.EntityFrameworkCore;
using ZlecajGo.Application.OfferContractors.Dtos;
using ZlecajGo.Domain.Constants;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;
using ZlecajGo.Infrastructure.Persistence;

namespace ZlecajGo.Infrastructure.Repositories;

internal class OfferContractorRepository(ZlecajGoContext dbContext) : IOfferContractorRepository
{
    public async Task<IEnumerable<OfferContractor>> GetProvidedOffersWithContractorAsync(string providerId)
    {
        var providedOffers = await dbContext.OfferContractors
            .AsNoTracking()
            .Where(oc => oc.Offer.ProviderId == providerId)
            .ToListAsync();

        return providedOffers;
    }

    public async Task<OfferContractor?> GetProvidedOfferWithContractorByHisIdAsync(string providerId, string contractorId)
    {
        var providedOffer = await dbContext.OfferContractors
            .AsNoTracking()
            .FirstOrDefaultAsync(oc => oc.Offer.ProviderId == providerId &&
                                       oc.ContractorId == contractorId);
        
        return providedOffer;
    }
    
    public async Task<IEnumerable<OfferContractor>> GetContractedOffersAsync(string contractorId)
    {
        var contractedOffers = await dbContext.OfferContractors
            .AsNoTracking()
            .Where(oc => oc.ContractorId == contractorId)
            .ToListAsync();

        return contractedOffers;
    }

    public async Task<OfferContractor?> GetContractedOfferByIdAsync(Guid offerId, string contractorId)
    {
        var contractedOffer = await dbContext.OfferContractors
            .AsNoTracking()
            .FirstOrDefaultAsync(oc => oc.OfferId == offerId &&
                                       oc.ContractorId == contractorId);

        return contractedOffer;
    }

    public async Task<OfferContractor?> GetOfferContractorByIdWithTrackingAsync(Guid offerId, string contractorId)
    {
        var offerContractor = await dbContext.OfferContractors
            .FirstOrDefaultAsync(oc => oc.OfferId == offerId &&
                                       oc.ContractorId == contractorId);

        return offerContractor;
    }

    public async Task<bool> ContractUserToOfferAsync(OfferContractor entity)
    {
        var offerId = entity.OfferId;
        var contractorId = entity.ContractorId;
        
        if (await IsOfferOccupiedAsync(offerId, contractorId))
            return false;
        
        var cancelledContractorContract = await GetCancelledContractForContractorAsync(offerId, contractorId);

        if (cancelledContractorContract != null)
        {
            await UpdateContractStatusToPlannedAsync(cancelledContractorContract);
            await UpdateOfferStatusToTaken();
            await SaveChangesAsync();
            return true;
        }

        await dbContext.OfferContractors.AddAsync(entity);
        await UpdateOfferStatusToTaken();
        await SaveChangesAsync();

        return true;

        async Task UpdateOfferStatusToTaken()
        {
            var offer = await dbContext.Offers
                .FirstOrDefaultAsync(o => o.Id == offerId);

            if (offer != null)
            {
                offer.StatusId = 2;
                dbContext.Update(offer);
            }
        }
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    
    private async Task<bool> IsOfferOccupiedAsync(Guid offerId, string contractorId)
    {
        var isOccupied = await dbContext.OfferContractors
            .AsNoTracking()
            .AnyAsync(oc => oc.OfferId == offerId &&
                            oc.StatusId != AppStatuses.Cancelled.Id);

        return isOccupied;
    }
    
    private async Task<OfferContractor?> GetCancelledContractForContractorAsync(Guid offerId, string contractorId)
    {
        var cancelledContract = await dbContext.OfferContractors
            .FirstOrDefaultAsync(oc => oc.OfferId == offerId &&
                                       oc.ContractorId == contractorId &&
                                       oc.StatusId == AppStatuses.Cancelled.Id);

        return cancelledContract;
    }
    
    private async Task UpdateContractStatusToPlannedAsync(OfferContractor cancelledContract)
    {
        cancelledContract.StatusId = AppStatuses.Planned.Id;
        cancelledContract.Status = await dbContext.Statuses.FirstAsync(s => s.Id == AppStatuses.Planned.Id);

        dbContext.Update(cancelledContract);
        await dbContext.SaveChangesAsync();
    }
}