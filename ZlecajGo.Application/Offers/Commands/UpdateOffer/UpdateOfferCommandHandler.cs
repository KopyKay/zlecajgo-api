using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Offers.Commands.UpdateOffer;

public class UpdateOfferCommandHandler
(
    ILogger<UpdateOfferCommandHandler> logger,
    IOfferRepository offerRepository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<UpdateOfferCommand>
{
    public async Task Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        
        logger.LogInformation("Updating offer with id [{OfferId}]", request.OfferId);

        var offer = await offerRepository.GetOfferByIdWithTrackingAsync(request.OfferId)
            ?? throw new NotFoundException(nameof(Offer), request.OfferId.ToString());

        if (offer.ProviderId != user!.Id)
            throw new NotAllowedException();
        
        mapper.Map(request, offer);
        
        await offerRepository.SaveChangesAsync();
    }
}