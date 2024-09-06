using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Users;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Reviews.Commands.CreateReview;

public class CreateReviewCommandHandler 
(
    ILogger<CreateReviewCommandHandler> logger,
    IReviewRepository reviewRepository,
    IUserStore<User> userStore,
    IUserContext userContext,
    IMapper mapper
)    
: IRequestHandler<CreateReviewCommand, bool>
{
    public async Task<bool> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        request.ReviewerId = user!.Id;
        
        var revieweeId = request.RevieweeId;
        
        logger.LogInformation("Creating review {@Review}", request);
        
        _ = await userStore.FindByIdAsync(revieweeId, cancellationToken)
            ?? throw new NotFoundException(nameof(User), revieweeId);
        
        var review = mapper.Map<Review>(request);
        var result = await reviewRepository.CreateReviewAsync(review);

        return result;
    }
}