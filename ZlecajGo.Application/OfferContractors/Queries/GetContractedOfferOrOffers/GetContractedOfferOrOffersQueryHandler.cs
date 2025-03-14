using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using ZlecajGo.Application.OfferContractors.Dtos;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.OfferContractors.Queries.GetContractedOfferOrOffers;

public class GetContractedOfferOrOffersQueryHandler 
(
    ILogger<GetContractedOfferOrOffersQueryHandler> logger,
    IUserContext userContext,
    IOfferContractorRepository offerContractorRepository,
    IMapper mapper
)     
: IRequestHandler<GetContractedOfferOrOffersQuery, OneOf<OfferContractorDto, IEnumerable<OfferContractorDto>>>
{
    public async Task<OneOf<OfferContractorDto, IEnumerable<OfferContractorDto>>> Handle(GetContractedOfferOrOffersQuery request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()!;
        
        if (request.OfferId.HasValue)
        {
            var offerId = request.OfferId.Value;
            var contractorId = user.Id;
        
            logger.LogInformation("Getting contracted offer with offer id [{OfferId}] and contractor id [{ContractorId}]", 
                offerId, contractorId);

            var contractedOffer = await offerContractorRepository.GetContractedOfferByIdAsync(offerId, contractorId)
                                  ?? throw new NotFoundException(nameof(OfferContractor), $"{offerId}] and [{contractorId}");
        
            var contractedOfferDto = mapper.Map<OfferContractorDto>(contractedOffer);
        
            return OneOf<OfferContractorDto, IEnumerable<OfferContractorDto>>.FromT0(contractedOfferDto);
        }
        
        logger.LogInformation("Getting all contracted offers for user with id [{UserId}]", user.Id);

        var contractedOffers = await offerContractorRepository.GetContractedOffersAsync(user.Id);
        
        var contractedOffersDto = mapper.Map<IEnumerable<OfferContractorDto>>(contractedOffers);

        return OneOf<OfferContractorDto, IEnumerable<OfferContractorDto>>.FromT1(contractedOffersDto);
    }
}