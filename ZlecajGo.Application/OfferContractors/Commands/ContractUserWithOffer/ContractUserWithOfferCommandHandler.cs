using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.OfferContractors.Commands.ContractUserWithOffer;

public class ContractUserWithOfferCommandHandler
(
    ILogger<ContractUserWithOfferCommandHandler> logger,
    IOfferContractorRepository offerContractorRepository,
    IOfferRepository offerRepository,
    IUserContext userContext,
    IMapper mapper
)        
: IRequestHandler<ContractUserWithOfferCommand, bool>
{
    public async Task<bool> Handle(ContractUserWithOfferCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        request.ContractorId = user!.Id;
        var contractorId = request.ContractorId;
        var offerId = request.OfferId;
        
        logger.LogInformation("Contracting user with id [{ContractorId}] with offer with id [{OfferId}]",
            contractorId, offerId);
        
        _ = await offerRepository.GetOfferByIdAsync(offerId)
            ?? throw new NotFoundException(nameof(Offer), offerId.ToString());
        
        var contractedOffer = mapper.Map<OfferContractor>(request);
        
        var result = await offerContractorRepository.ContractUserToOfferAsync(contractedOffer);

        return result;
    }
}