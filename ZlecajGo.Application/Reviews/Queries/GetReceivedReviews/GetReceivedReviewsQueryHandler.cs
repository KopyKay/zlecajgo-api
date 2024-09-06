using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Reviews.Dtos;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Reviews.Queries.GetReceivedReviews;

public class GetReceivedReviewsQueryHandler 
(
    ILogger<GetReceivedReviewsQueryHandler> logger,
    IReviewRepository reviewRepository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<GetReceivedReviewsQuery, IEnumerable<ReviewDto>>
{
    public async Task<IEnumerable<ReviewDto>> Handle(GetReceivedReviewsQuery request, CancellationToken cancellationToken)
    {
        var revieweeId = string.IsNullOrEmpty(request.UserId) ? userContext.GetCurrentUser()!.Id : request.UserId;
        
        logger.LogInformation("Getting received reviews for user with id [{UserId}]", revieweeId);
        
        var receivedReviews = await reviewRepository.GetReceivedReviewsAsync(revieweeId);
        var receivedReviewsDto = mapper.Map<IEnumerable<ReviewDto>>(receivedReviews);
        
        return receivedReviewsDto;
    }
}