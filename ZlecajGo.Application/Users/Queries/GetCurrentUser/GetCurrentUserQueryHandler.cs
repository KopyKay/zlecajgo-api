using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ZlecajGo.Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler 
(
    ILogger<GetCurrentUserQueryHandler> logger,
    IUserContext userContext,
    IMapper mapper
)
: IRequestHandler<GetCurrentUserQuery, CurrentUser>
{
    public Task<CurrentUser> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()!;
        
        logger.LogInformation("Getting current user with id [{UserId}]", user.Id);

        return Task.FromResult(user);
    }
}