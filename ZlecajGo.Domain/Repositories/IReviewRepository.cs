using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Domain.Repositories;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetReviewsAsync();
    Task<IEnumerable<Review>> GetReceivedReviewsAsync(string revieweeId);
    Task<IEnumerable<Review>> GetWrittenReviewsAsync(string reviewerId);
    Task<Review?> GetReceivedReviewFromUserAsync(string userId, string reviewerId);
    Task<Review?> GetWrittenReviewForUserAsync(string userId, string revieweeId);
    Task<Review?> GetWrittenReviewForUserWithTrackingAsync(string userId, string revieweeId);
    Task<bool> CreateReviewAsync(Review entity);
    Task DeleteReviewAsync(Review entity);
    Task SaveChangesAsync();
}