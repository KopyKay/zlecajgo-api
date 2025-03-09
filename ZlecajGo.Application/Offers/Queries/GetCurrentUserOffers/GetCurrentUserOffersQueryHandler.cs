using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Offers.Dtos;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Offers.Queries.GetCurrentUserOffers;

public class GetCurrentUserOffersQueryHandler 
(
    ILogger<GetCurrentUserOffersQueryHandler> logger,
    IUserContext userContext,
    IOfferRepository offerRepository,
    IMapper mapper
)
: IRequestHandler<GetCurrentUserOffersQuery, IEnumerable<OfferDto>>
{
    public async Task<IEnumerable<OfferDto>> Handle(GetCurrentUserOffersQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser()!;
        var userId = currentUser.Id;
        
        logger.LogInformation("Getting all offers provided by user with id [{UserId}]", userId);

        var offers = await offerRepository.GetUserOffersAsync(userId);
        var offersDto = mapper.Map<IEnumerable<OfferDto>>(offers);

        return offersDto;
    }
}