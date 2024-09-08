using Microsoft.EntityFrameworkCore;
using ZlecajGo.Domain.Constants;
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
            return true;
        }

        await dbContext.OfferContractors.AddAsync(entity);
        await SaveChangesAsync();

        return true;
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