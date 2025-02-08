using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Offers.Commands.UpdateOfferStatus;

public class UpdateOfferStatusCommandHandler 
(
    ILogger<UpdateOfferStatusCommandHandler> logger,
    IOfferRepository offerRepository,
    IUserContext userContext,
    IMapper mapper
)     
: IRequestHandler<UpdateOfferStatusCommand>
{
    public async Task Handle(UpdateOfferStatusCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()!;
        
        logger.LogInformation("Updating offer status {@Offer}", request);
        
        var offer = await offerRepository.GetOfferByIdWithTrackingAsync(request.OfferId)
            ?? throw new NotFoundException(nameof(Offer), request.OfferId.ToString());
        
        if (offer.ProviderId != user.Id)
            throw new NotAllowedException();
        
        mapper.Map(request, offer);

        await offerRepository.SaveChangesAsync();
    }
}