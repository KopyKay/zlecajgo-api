using Microsoft.EntityFrameworkCore;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;
using ZlecajGo.Infrastructure.Persistence;

namespace ZlecajGo.Infrastructure.Repositories;

internal class ReviewRepository(ZlecajGoContext dbContext) : IReviewRepository
{
    public async Task<IEnumerable<Review>> GetReviewsAsync()
    {
        var reviews = await dbContext.Reviews
            .AsNoTracking()
            .ToListAsync();

        return reviews;
    }

    public async Task<IEnumerable<Review>> GetReceivedReviewsAsync(string revieweeId)
    {
        var reviews = await dbContext.Reviews
            .AsNoTracking()
            .Where(r => r.RevieweeId == revieweeId)
            .ToListAsync();
        
        return reviews;
    }

    public async Task<IEnumerable<Review>> GetWrittenReviewsAsync(string reviewerId)
    {
        var reviews = await dbContext.Reviews
            .AsNoTracking()
            .Where(r => r.ReviewerId == reviewerId)
            .ToListAsync();
        
        return reviews;
    }

    public async Task<Review?> GetReceivedReviewFromUserAsync(string userId, string reviewerId)
    {
        var review = await dbContext.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ReviewerId == reviewerId && r.RevieweeId == userId);
        
        return review;
    }

    public async Task<Review?> GetWrittenReviewForUserAsync(string userId, string revieweeId)
    {
        var review = await dbContext.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ReviewerId == userId && r.RevieweeId == revieweeId);
        
        return review;
    }

    public async Task<Review?> GetWrittenReviewForUserWithTrackingAsync(string userId, string revieweeId)
    {
        var review = await dbContext.Reviews
            .FirstOrDefaultAsync(r => r.ReviewerId == userId && r.RevieweeId == revieweeId);
        
        return review;
    }

    public async Task<bool> CreateReviewAsync(Review entity)
    {
        var reviewerId = entity.ReviewerId;
        var revieweeId = entity.RevieweeId;
        
        if (await IsReviewerAlreadyReviewedRevieweeAsync(reviewerId, revieweeId) || 
            !await IsReviewerRelationWithRevieweeExistAsync(reviewerId, revieweeId))
        {
            return false;
        }
        
        await dbContext.Reviews.AddAsync(entity);
        await SaveChangesAsync();

        return true;
    }

    public async Task DeleteReviewAsync(Review entity)
    {
        dbContext.Reviews.Remove(entity);
        await SaveChangesAsync();
    }
    
    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    
    // Method to check if reviewer already reviewed reviewee
    private Task<bool> IsReviewerAlreadyReviewedRevieweeAsync(string reviewerId, string revieweeId)
    {
        return dbContext.Reviews
            .AsNoTracking()
            .AnyAsync(r => r.ReviewerId == reviewerId && r.RevieweeId == revieweeId);
    }

    // Method to check if user is allowed to review another user by checking if they have a contract
    private async Task<bool> IsReviewerRelationWithRevieweeExistAsync(string reviewerId, string revieweeId)
    {
        var isContracted = await dbContext.Offers
            .AsNoTracking()
            .Where(o => o.ProviderId == revieweeId)
            .Join(dbContext.OfferContractors,
                offer => offer.Id,
                contractor => contractor.OfferId,
                (offer, contractor) => contractor)
            .AnyAsync(oc => oc.ContractorId == reviewerId);

        return isContracted;
    }
}