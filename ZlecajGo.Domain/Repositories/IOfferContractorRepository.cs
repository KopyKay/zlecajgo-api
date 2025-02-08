using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Domain.Repositories;

public interface IOfferContractorRepository
{
    Task<IEnumerable<OfferContractor>> GetContractedOffersAsync(string userId);
    Task<OfferContractor?> GetContractedOfferByIdAsync(Guid offerId, string userId);
    Task<OfferContractor?> GetContractedOfferByIdWithTrackingAsync(Guid offerId, string userId);
    Task<bool> ContractUserToOfferAsync(OfferContractor entity);
    Task SaveChangesAsync();
}