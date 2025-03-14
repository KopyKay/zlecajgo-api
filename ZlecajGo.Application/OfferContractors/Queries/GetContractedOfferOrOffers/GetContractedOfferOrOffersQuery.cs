using MediatR;
using OneOf;
using ZlecajGo.Application.OfferContractors.Dtos;

namespace ZlecajGo.Application.OfferContractors.Queries.GetContractedOfferOrOffers;

public record GetContractedOfferOrOffersQuery(Guid? OfferId) : IRequest<OneOf<OfferContractorDto, IEnumerable<OfferContractorDto>>>;