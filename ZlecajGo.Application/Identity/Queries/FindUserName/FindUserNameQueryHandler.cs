using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;

namespace ZlecajGo.Application.Identity.Queries.FindUserName;

public class FindUserNameQueryHandler
(
    ILogger<FindUserNameQueryHandler> logger,
    UserManager<User> userManager
)
: IRequestHandler<FindUserNameQuery, string>
{
    public async Task<string> Handle(FindUserNameQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Finding user name for email [{Email}]", request.Email);
        
        var userName = await userManager.FindByEmailAsync(request.Email)
            ?? throw new NotFoundException(nameof(User), request.Email);

        return userName.UserName!;
    }
}