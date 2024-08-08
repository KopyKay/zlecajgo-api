using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Offers.Dtos;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Offers.Queries.GetOffer;

public class GetOfferQueryHandler
(
    ILogger<GetOfferQueryHandler> logger,
    IOfferRepository offerRepository,
    IMapper mapper
)    
: IRequestHandler<GetOfferQuery, OfferDto>
{
    public async Task<OfferDto> Handle(GetOfferQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting offer with id: {OfferId}", request.OfferId);
        
        var offer = await offerRepository.GetOfferByIdAsync(request.OfferId)
            ?? throw new NotFoundException(nameof(Offer), request.OfferId.ToString());
        
        var offerDto = mapper.Map<OfferDto>(offer);
        
        return offerDto;
    }
}