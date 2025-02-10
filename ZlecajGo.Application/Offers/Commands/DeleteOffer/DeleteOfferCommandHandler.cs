using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Offers.Commands.DeleteOffer;

public class DeleteOfferCommandHandler
(
    ILogger<DeleteOfferCommandHandler> logger,
    IOfferRepository offerRepository,
    IUserContext userContext
)    
: IRequestHandler<DeleteOfferCommand, bool>
{
    public async Task<bool> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()!;
        
        logger.LogInformation("Deleting offer with id [{OfferId}]", request.OfferId);
        
        var offer = await offerRepository.GetOfferByIdAsync(request.OfferId)
            ?? throw new NotFoundException(nameof(Offer), request.OfferId.ToString());
        
        if (offer.ProviderId != user.Id)
            throw new NotAllowedException();
        
        var result = await offerRepository.DeleteOfferAsync(offer);
        return result;
    }
}