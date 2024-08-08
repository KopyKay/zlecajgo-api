using MediatR;
using ZlecajGo.Application.Offers.Dtos;

namespace ZlecajGo.Application.Offers.Queries.GetOffer;

public record GetOfferQuery(Guid OfferId) : IRequest<OfferDto>;