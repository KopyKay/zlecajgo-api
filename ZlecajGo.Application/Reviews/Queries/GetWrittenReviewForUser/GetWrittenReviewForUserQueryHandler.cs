using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Reviews.Dtos;
using ZlecajGo.Application.Reviews.Queries.GetReceivedReviewFromUser;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Reviews.Queries.GetWrittenReviewForUser;

public class GetWrittenReviewForUserQueryHandler 
(
    ILogger<GetReceivedReviewFromUserQueryHandler> logger,
    IReviewRepository reviewRepository,
    IUserStore<User> userStore,
    IUserContext userContext,
    IMapper mapper
)     
: IRequestHandler<GetWrittenReviewForUserQuery, ReviewDto>
{
    public async Task<ReviewDto> Handle(GetWrittenReviewForUserQuery request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()!;
        var reviewerId = user.Id;
        var revieweeId = request.UserId;
        
        logger.LogInformation("Getting review written to user with id [{RevieweeId}] from user with id [{ReviewerId}]", 
            revieweeId, reviewerId);
        
        _ = await userStore.FindByIdAsync(revieweeId, cancellationToken)
            ?? throw new NotFoundException(nameof(User), revieweeId);
        
        var writtenReview = await reviewRepository.GetWrittenReviewForUserAsync(reviewerId, revieweeId)
            ?? throw new NotFoundException(nameof(Review), $"of reviewer {reviewerId} and reviewee {revieweeId}");
            
        var writtenReviewDto = mapper.Map<ReviewDto>(writtenReview);
        
        return writtenReviewDto;
    }
}