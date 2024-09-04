using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.OfferContractors.Dtos;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.OfferContractors.Queries.GetContractedOffers;

public class GetContractedOffersQueryHandler 
(
    ILogger<GetContractedOffersQueryHandler> logger,
    IOfferContractorRepository offerContractorRepository,
    IMapper mapper
)     
: IRequestHandler<GetContractedOffersQuery, IEnumerable<OfferContractorDto>>
{
    public async Task<IEnumerable<OfferContractorDto>> Handle(GetContractedOffersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all contracted offers");
        
        var contractedOffers = await offerContractorRepository.GetContractedOffersAsync();
        var contractedOffersDto = mapper.Map<IEnumerable<OfferContractorDto>>(contractedOffers);

        return contractedOffersDto;
    }
}