using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Reviews.Dtos;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Reviews.Queries.GetReviews;

public class GetReviewsQueryHandler 
(
    ILogger<GetReviewsQueryHandler> logger,
    IReviewRepository reviewRepository,
    IMapper mapper
)    
: IRequestHandler<GetReviewsQuery, IEnumerable<ReviewDto>>
{
    public async Task<IEnumerable<ReviewDto>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all reviews");

        var reviews = await reviewRepository.GetReviewsAsync();
        var reviewsDto = mapper.Map<IEnumerable<ReviewDto>>(reviews);

        return reviewsDto;
    }
}