using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Domain.Exceptions;

public class OfferOccupiedException(Guid offerId, string contractorId) 
    : Exception($"{nameof(Offer)} with identifier [{offerId}] is occupied by contractor with identifier [{contractorId}].");