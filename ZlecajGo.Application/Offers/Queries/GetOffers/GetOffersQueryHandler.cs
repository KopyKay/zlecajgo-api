using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Offers.Dtos;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Offers.Queries.GetOffers;

public class GetOffersQueryHandler
(
    ILogger<GetOffersQueryHandler> logger,
    IOfferRepository offerRepository,
    IMapper mapper
)
: IRequestHandler<GetOffersQuery, IEnumerable<OfferDto>>
{
    public async Task<IEnumerable<OfferDto>> Handle(GetOffersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all offers");

        var offers = await offerRepository.GetOffersAsync();
        var offersDto = mapper.Map<IEnumerable<OfferDto>>(offers);

        return offersDto;
    }
}