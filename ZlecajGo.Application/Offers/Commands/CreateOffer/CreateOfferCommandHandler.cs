using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Offers.Commands.CreateOffer;

public class CreateOfferCommandHandler
(
    ILogger<CreateOfferCommandHandler> logger,
    IOfferRepository offerRepository,
    IMapper mapper
)    
: IRequestHandler<CreateOfferCommand, Guid>
{
    public async Task<Guid> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new offer {@Offer}", request);
        
        var offer = mapper.Map<Offer>(request);
        var offerId = await offerRepository.CreateOfferAsync(offer);

        return offerId;
    }
}