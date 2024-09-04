using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Offers.Commands.UpdateOffer;

public class UpdateOfferCommandHandler
(
    ILogger<UpdateOfferCommandHandler> logger,
    IOfferRepository offerRepository,
    IMapper mapper
)    
: IRequestHandler<UpdateOfferCommand, bool>
{
    public async Task<bool> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating offer with ID: {OfferId}", request.OfferId);

        var offer = await offerRepository.GetOfferByIdWithTrackingAsync(request.OfferId);
        
        if (offer is null)
            return false;
        
        mapper.Map(request, offer);
        
        await offerRepository.SaveChangesAsync();

        return true;
    }
}