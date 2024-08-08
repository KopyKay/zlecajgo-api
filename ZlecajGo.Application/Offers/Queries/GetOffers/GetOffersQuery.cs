using MediatR;
using ZlecajGo.Application.Offers.Dtos;

namespace ZlecajGo.Application.Offers.Queries.GetOffers;

public class GetOffersQuery : IRequest<IEnumerable<OfferDto>>;