using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Reviews.Dtos;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Reviews.Queries.GetWrittenReviews;

public class GetWrittenReviewsQueryHandler
(
    ILogger<GetWrittenReviewsQueryHandler> logger,
    IReviewRepository reviewRepository,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<GetWrittenReviewsQuery, IEnumerable<ReviewDto>>
{
    public async Task<IEnumerable<ReviewDto>> Handle(GetWrittenReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviewerId = string.IsNullOrEmpty(request.UserId) ? userContext.GetCurrentUser()!.Id : request.UserId;
        
        logger.LogInformation("Getting written reviews for user with id [{UserId}]", reviewerId);
        
        var writtenReviews = await reviewRepository.GetWrittenReviewsAsync(reviewerId);
        var writtenReviewsDto = mapper.Map<IEnumerable<ReviewDto>>(writtenReviews);
        
        return writtenReviewsDto;
    }
}