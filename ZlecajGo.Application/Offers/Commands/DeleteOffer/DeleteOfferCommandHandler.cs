using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Offers.Commands.DeleteOffer;

public class DeleteOfferCommandHandler
(
    ILogger<DeleteOfferCommandHandler> logger,
    IOfferRepository offerRepository,
    IOfferContractorRepository offerContractorRepository
)    
: IRequestHandler<DeleteOfferCommand, bool>
{
    public async Task<bool> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting offer with id [{OfferId}]", request.OfferId);
        
        var offer = await offerRepository.GetOfferByIdAsync(request.OfferId)
            ?? throw new NotFoundException(nameof(Offer), request.OfferId.ToString());
        
        var hasOfferBeenPerformed = await offerContractorRepository.HasOfferBeenPerformedAsync(request.OfferId);

        if (hasOfferBeenPerformed) return false;
        
        await offerRepository.DeleteOfferAsync(offer);
        return true;
    }
}