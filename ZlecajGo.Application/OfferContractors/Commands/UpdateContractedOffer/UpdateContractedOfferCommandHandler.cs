using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
    IUserStore<User> userStore,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<UpdateContractedOfferCommand>
{
    public async Task Handle(UpdateContractedOfferCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()!;
        
        var contractorId = request.ContractorId;
        var offerId = request.OfferId;
        
        logger.LogInformation("Updating contracted offer with id [{OfferId}] for contractor with id [{ContractorId}]",
            offerId, contractorId);

        _ = await userStore.FindByIdAsync(contractorId, cancellationToken)
            ?? throw new NotFoundException(nameof(User), contractorId);
        
        var offer = await offerRepository.GetOfferByIdAsync(offerId)
            ?? throw new NotFoundException(nameof(Offer), offerId.ToString());

        var contractedOffer = await offerContractorRepository
            .GetContractedOfferByIdWithTrackingAsync(offerId, contractorId)
            ?? throw new NotFoundException(nameof(OfferContractor), $"{offerId}] and [{contractorId}");

        if (contractedOffer.ContractorId != user.Id)
        {
            if (offer.ProviderId != user.Id && contractedOffer.ContractorId != contractorId)
            {
                throw new NotAllowedException();
            }
        }
        
        mapper.Map(request, contractedOffer);
        
        await offerContractorRepository.SaveChangesAsync();
    }
}