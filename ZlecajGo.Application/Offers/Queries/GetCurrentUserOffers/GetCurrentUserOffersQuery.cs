using MediatR;
using ZlecajGo.Application.Offers.Dtos;

namespace ZlecajGo.Application.Offers.Queries.GetCurrentUserOffers;

public class GetCurrentUserOffersQuery : IRequest<IEnumerable<OfferDto>>;