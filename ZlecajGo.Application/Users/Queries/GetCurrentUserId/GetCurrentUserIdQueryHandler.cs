using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ZlecajGo.Application.Users.Queries.GetCurrentUserId;

public class GetCurrentUserIdQueryHandler
(
    ILogger<GetCurrentUserIdQueryHandler> logger,
    IUserContext userContext
)
: IRequestHandler<GetCurrentUserIdQuery, string>
{
    public Task<string> Handle(GetCurrentUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        var userId = user!.Id;
        
        logger.LogInformation("Getting current user id {UserId}", userId);

        return Task.FromResult(userId);
    }
}