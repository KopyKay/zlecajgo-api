using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Domain.Repositories;

public interface IOfferRepository
{
    Task<IEnumerable<Offer>> GetOffersAsync();
    Task<Offer?> GetOfferByIdAsync(Guid offerId);
    Task<Offer?> GetOfferByIdWithTrackingAsync(Guid offerId);
    Task<Guid> CreateOfferAsync(Offer entity);
    Task DeleteOfferAsync(Offer entity);
    Task<bool> HasOfferBeenPerformedAsync(Guid offerId);
    Task SaveChangesAsync();
}