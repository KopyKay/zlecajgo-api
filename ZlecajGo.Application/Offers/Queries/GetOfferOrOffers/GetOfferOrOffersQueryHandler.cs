using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using ZlecajGo.Application.Offers.Dtos;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Offers.Queries.GetOfferOrOffers;

public class GetOfferOrOffersQueryHandler
(
    ILogger<GetOfferOrOffersQueryHandler> logger,
    IOfferRepository offerRepository,
    IMapper mapper
)
: IRequestHandler<GetOfferOrOffersQuery, OneOf<OfferDto, IEnumerable<OfferDto>>>
{
    public async Task<OneOf<OfferDto, IEnumerable<OfferDto>>> Handle(GetOfferOrOffersQuery request, CancellationToken cancellationToken)
    {
        if (request.OfferId.HasValue)
        {
            var offerId = request.OfferId.Value;
            
            logger.LogInformation("Getting offer with id [{OfferId}]", offerId);
        
            var offer = await offerRepository.GetOfferByIdAsync(offerId)
                        ?? throw new NotFoundException(nameof(Offer), offerId.ToString());
        
            var offerDto = mapper.Map<OfferDto>(offer);
        
            return OneOf<OfferDto, IEnumerable<OfferDto>>.FromT0(offerDto);
        }
        
        logger.LogInformation("Getting all offers");

        var offers = await offerRepository.GetOffersAsync();
        var offersDto = mapper.Map<IEnumerable<OfferDto>>(offers);

        return OneOf<OfferDto, IEnumerable<OfferDto>>.FromT1(offersDto);
    }
}