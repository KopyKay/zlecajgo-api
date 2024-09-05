using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.OfferContractors.Commands.UpdateContractedOffer;

public class UpdateContractedOfferCommandHandler
(
    ILogger<UpdateContractedOfferCommandHandler> logger,
    IOfferContractorRepository offerContractorRepository,
    IOfferRepository offerRepository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<UpdateContractedOfferCommand>
{
    public async Task Handle(UpdateContractedOfferCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        request.ContractorId = user!.Id;
        var contractorId = request.ContractorId;
        var offerId = request.OfferId;
        
        logger.LogInformation("Updating contracted offer with id [{OfferId}] for contractor with id [{ContractorId}]",
            offerId, contractorId);

        _ = await offerRepository.GetOfferByIdAsync(offerId)
            ?? throw new NotFoundException(nameof(Offer), offerId.ToString());

        var contractedOffer = await offerContractorRepository
            .GetContractedOfferByIdWithTrackingAsync(offerId, contractorId)
            ?? throw new NotFoundException(nameof(OfferContractor), $"{offerId} and {contractorId}");
        
        mapper.Map(request, contractedOffer);
        
        await offerContractorRepository.SaveChangesAsync();
    }
}