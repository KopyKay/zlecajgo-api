using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Reviews.Commands.UpdateReview;

public class UpdateReviewCommandHandler 
(
    ILogger<UpdateReviewCommandHandler> logger,
    IReviewRepository reviewRepository,
    IUserStore<User> userStore,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<UpdateReviewCommand>
{
    public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        request.ReviewerId = user!.Id;
        
        var reviewerId = request.ReviewerId;
        var revieweeId = request.RevieweeId;
        
        logger.LogInformation("Updating review with {@Review}", request);

        _ = await userStore.FindByIdAsync(revieweeId, cancellationToken)
            ?? throw new NotFoundException(nameof(User), revieweeId);
        
        var review = await reviewRepository.GetWrittenReviewForUserWithTrackingAsync(reviewerId, revieweeId)
                     ?? throw new NotFoundException(nameof(Review),
                         $"of user who was reviewed with id {reviewerId}");
        
        mapper.Map(request, review);

        await reviewRepository.SaveChangesAsync();
    }
}