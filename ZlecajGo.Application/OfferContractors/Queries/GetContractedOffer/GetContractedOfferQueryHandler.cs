using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.OfferContractors.Dtos;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.OfferContractors.Queries.GetContractedOffer;

public class GetContractedOfferQueryHandler
(
    ILogger<GetContractedOfferQueryHandler> logger,
    IOfferContractorRepository offerContractorRepository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<GetContractedOfferQuery, OfferContractorDto>
{
    public async Task<OfferContractorDto> Handle(GetContractedOfferQuery request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        var contractorId = user!.Id;
        var offerId = request.OfferId;
        
        logger.LogInformation("Getting contracted offer with offer id [{OfferId}] and contractor id [{ContractorId}]", 
            offerId, contractorId);

        var contractedOffer = await offerContractorRepository.GetContractedOfferByIdAsync(offerId, contractorId)
            ?? throw new NotFoundException(nameof(OfferContractor), $"{offerId} and {contractorId}");
        
        if (contractedOffer.Offer.ProviderId != user.Id ||
            contractedOffer.ContractorId != user.Id)
            throw new NotAllowedException();
        
        var contractedOfferDto = mapper.Map<OfferContractorDto>(contractedOffer);
        
        return contractedOfferDto;
    }
}