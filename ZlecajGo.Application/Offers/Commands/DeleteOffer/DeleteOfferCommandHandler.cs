using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Offers.Commands.DeleteOffer;

public class DeleteOfferCommandHandler
(
    ILogger<DeleteOfferCommandHandler> logger,
    IOfferRepository offerRepository
)    
: IRequestHandler<DeleteOfferCommand, bool>
{
    public async Task<bool> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting offer with id [{OfferId}]", request.OfferId);
        
        var offer = await offerRepository.GetOfferByIdAsync(request.OfferId)
            ?? throw new NotFoundException(nameof(Offer), request.OfferId.ToString());
        
        var result = await offerRepository.DeleteOfferAsync(offer);
        return result;
    }
}