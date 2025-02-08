using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Reviews.Commands.DeleteReview;

public class DeleteReviewCommandHandler 
(
    ILogger<DeleteReviewCommandHandler> logger,
    IReviewRepository reviewRepository,
    IUserStore<User> userStore,
    IUserContext userContext
)    
: IRequestHandler<DeleteReviewCommand>
{
    public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        var reviewerId = user!.Id;
        var revieweeId = request.RevieweeId;
        
        logger.LogInformation("Deleting review for user with id [{Reviewee}] by user with id [{Reviewer}]", 
            revieweeId, reviewerId);

        _ = await userStore.FindByIdAsync(revieweeId, cancellationToken)
            ?? throw new NotFoundException(nameof(User), revieweeId);
        
        var review = await reviewRepository.GetWrittenReviewForUserAsync(reviewerId, revieweeId)
                     ?? throw new NotFoundException(nameof(Review),
                         $"of user who was reviewed with id {reviewerId}");

        if (review.ReviewerId != user.Id)
            throw new NotAllowedException();
        
        await reviewRepository.DeleteReviewAsync(review);
    }
}