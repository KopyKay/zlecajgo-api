using System.Diagnostics;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Offers.Dtos;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Offers.Commands.UpdateOfferStatus;

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