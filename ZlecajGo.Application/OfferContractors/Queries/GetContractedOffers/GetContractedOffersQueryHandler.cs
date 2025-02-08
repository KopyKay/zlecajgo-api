using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.OfferContractors.Dtos;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.OfferContractors.Queries.GetContractedOffers;

public class GetContractedOffersQueryHandler 
(
    ILogger<GetContractedOffersQueryHandler> logger,
    IUserContext userContext,
    IOfferContractorRepository offerContractorRepository,
    IMapper mapper
)     
: IRequestHandler<GetContractedOffersQuery, IEnumerable<OfferContractorDto>>
{
    public async Task<IEnumerable<OfferContractorDto>> Handle(GetContractedOffersQuery request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()!;
        
        logger.LogInformation("Getting all contracted offers for user with id [{UserId}]", user.Id);

        var contractedOffers = await offerContractorRepository.GetContractedOffersAsync(user.Id);
        
        var contractedOffersDto = mapper.Map<IEnumerable<OfferContractorDto>>(contractedOffers);

        return contractedOffersDto;
    }
}