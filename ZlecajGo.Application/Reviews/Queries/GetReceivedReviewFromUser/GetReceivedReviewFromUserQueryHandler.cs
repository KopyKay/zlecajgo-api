using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Reviews.Dtos;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Reviews.Queries.GetReceivedReviewFromUser;

public class GetReceivedReviewFromUserQueryHandler 
(
    ILogger<GetReceivedReviewFromUserQueryHandler> logger,
    IReviewRepository reviewRepository,
    IUserStore<User> userStore,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<GetReceivedReviewFromUserQuery, ReviewDto>
{
    public async Task<ReviewDto> Handle(GetReceivedReviewFromUserQuery request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        var reviewerId = request.UserId;
        var revieweeId = user!.Id;
        
        logger.LogInformation("Getting review written to user with id [{RevieweeId}] from user with id [{ReviewerId}]", 
            revieweeId, reviewerId);
        
        _ = await userStore.FindByIdAsync(revieweeId, cancellationToken)
            ?? throw new NotFoundException(nameof(User), reviewerId);
        
        var receivedReview = await reviewRepository.GetReceivedReviewFromUserAsync(revieweeId, reviewerId)
            ?? throw new NotFoundException(nameof(Review), $"of reviewer {reviewerId} and reviewee {revieweeId}");
            
        var receivedReviewDto = mapper.Map<ReviewDto>(receivedReview);
        
        return receivedReviewDto;
    }
}