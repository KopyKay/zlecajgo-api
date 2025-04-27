using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Domain.Repositories;

public interface IOfferContractorRepository
{
    Task<IEnumerable<OfferContractor>> GetProvidedOffersWithContractorAsync(string providerId);
    Task<OfferContractor?> GetProvidedOfferWithContractorByHisIdAsync(string providerId, string contractorId);
    Task<IEnumerable<OfferContractor>> GetContractedOffersAsync(string contractorId);
    Task<OfferContractor?> GetContractedOfferByIdAsync(Guid offerId, string contractorId);
    Task<OfferContractor?> GetOfferContractorByIdWithTrackingAsync(Guid offerId, string contractorId);
    Task<bool> ContractUserToOfferAsync(OfferContractor entity);
    Task SaveChangesAsync();
}