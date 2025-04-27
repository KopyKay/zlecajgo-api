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
        
        var offerId = request.OfferId;
        var contractorId = request.ContractorId;
        
        logger.LogInformation("Updating contracted offer with id [{OfferId}] and contractor with id [{ContractorId}]",
            offerId, contractorId);

        var offer = await offerRepository.GetOfferByIdAsync(offerId)
                    ?? throw new NotFoundException(nameof(Offer), offerId.ToString());
        
        _ = await userStore.FindByIdAsync(contractorId, cancellationToken)
            ?? throw new NotFoundException(nameof(User), contractorId);

        var contractedOffer = await offerContractorRepository
            .GetOfferContractorByIdWithTrackingAsync(offerId, contractorId)
            ?? throw new NotFoundException(nameof(OfferContractor), $"{offerId}] and [{contractorId}");

        var isUserProvider = user.Id == offer.ProviderId;
        var isUserContractor = user.Id == contractedOffer.ContractorId;

        if (!isUserProvider && !isUserContractor) throw new NotAllowedException();
        if (contractorId != contractedOffer.ContractorId) throw new NotAllowedException();
        
        mapper.Map(request, contractedOffer);
        
        await offerContractorRepository.SaveChangesAsync();
    }
}