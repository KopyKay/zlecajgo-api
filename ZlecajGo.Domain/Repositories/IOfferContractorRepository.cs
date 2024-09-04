using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Domain.Repositories;

public interface IOfferContractorRepository
{
    Task<IEnumerable<OfferContractor>> GetContractedOffersAsync();
    Task<OfferContractor?> GetContractedOfferByIdAsync(Guid offerId, string contractorId);
    Task<OfferContractor?> GetContractedOfferByIdWithTrackingAsync(Guid offerId, string contractorId);
    Task ContractUserToOfferAsync(OfferContractor entity);
    Task<bool> CheckIsContractedOfferOccupiedAsync(Guid offerId);
    Task SaveChangesAsync();
}