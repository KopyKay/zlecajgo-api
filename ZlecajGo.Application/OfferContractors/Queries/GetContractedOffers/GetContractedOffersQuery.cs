using MediatR;
using ZlecajGo.Application.OfferContractors.Dtos;

namespace ZlecajGo.Application.OfferContractors.Queries.GetContractedOffers;

public class GetContractedOffersQuery : IRequest<IEnumerable<OfferContractorDto>>;