using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
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
        logger.LogInformation("Updating offer with id [{OfferId}]", request.OfferId);

        var offer = await offerRepository.GetOfferByIdWithTrackingAsync(request.OfferId)
            ?? throw new NotFoundException(nameof(Offer), request.OfferId.ToString());
        
        mapper.Map(request, offer);
        
        await offerRepository.SaveChangesAsync();
        return true;
    }
}