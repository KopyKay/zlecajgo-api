using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using ZlecajGo.Application.OfferContractors.Dtos;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.OfferContractors.Queries.GetProvidedOfferOrOffersWithContractor;

public class GetProvidedOfferOrOffersWithContractorQueryHandler
(
    ILogger<GetProvidedOfferOrOffersWithContractorQueryHandler> logger,
    IUserContext userContext,
    IOfferContractorRepository offerContractorRepository,
    IMapper mapper
)
: IRequestHandler<GetProvidedOfferOrOffersWithContractorQuery, OneOf<OfferContractorDto, IEnumerable<OfferContractorDto>>>
{
    public async Task<OneOf<OfferContractorDto, IEnumerable<OfferContractorDto>>> Handle(GetProvidedOfferOrOffersWithContractorQuery request, CancellationToken cancellationToken)
    {
        var providerId = userContext.GetCurrentUser()!.Id;

        if (!string.IsNullOrEmpty(request.ContractorId))
        {
            var contractorId = request.ContractorId!;
            
            logger.LogInformation("Getting provided offer with provider id [{ProviderId}] and contractor id [{ContractorId}]",
                providerId, contractorId);
            
            var providedOffer = await offerContractorRepository.GetProvidedOfferWithContractorByHisIdAsync(providerId, contractorId)
                ?? throw new NotFoundException(nameof(OfferContractor), $"{providerId}] and [{contractorId}");
            
            var providedOfferDto = mapper.Map<OfferContractorDto>(providedOffer);
            
            return OneOf<OfferContractorDto, IEnumerable<OfferContractorDto>>.FromT0(providedOfferDto);
        }
        
        logger.LogInformation("Getting all provided offers with contractor for user with id [{UserId}]", providerId);
        
        var providedOffers = await offerContractorRepository.GetProvidedOffersWithContractorAsync(providerId);
        
        var providedOffersDto = mapper.Map<IEnumerable<OfferContractorDto>>(providedOffers);
        
        return OneOf<OfferContractorDto, IEnumerable<OfferContractorDto>>.FromT1(providedOffersDto);
    }
}