using MediatR;
using OneOf;
using ZlecajGo.Application.Offers.Dtos;

namespace ZlecajGo.Application.Offers.Queries.GetOfferOrOffers;

public record GetOfferOrOffersQuery(Guid? OfferId) : IRequest<OneOf<OfferDto, IEnumerable<OfferDto>>>;