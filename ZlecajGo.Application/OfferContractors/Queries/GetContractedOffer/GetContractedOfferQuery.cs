using MediatR;
using ZlecajGo.Application.OfferContractors.Dtos;

namespace ZlecajGo.Application.OfferContractors.Queries.GetContractedOffer;

public record GetContractedOfferQuery(Guid OfferId) : IRequest<OfferContractorDto>;