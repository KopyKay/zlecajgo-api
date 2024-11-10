using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Identity.Queries.IsUserNameExist;

public class IsUserNameExistQueryHandler 
(
    ILogger<IsUserNameExistQueryHandler> logger,
    UserManager<User> userManager
) 
: IRequestHandler<IsUserNameExistQuery, bool>
{
    public async Task<bool> Handle(IsUserNameExistQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Checking if a username [{UserName}] already exists", request.UserName);
        
        var isUserNameExist = await userManager.Users
            .AsNoTracking()
            .AnyAsync(u => u.UserName == request.UserName, cancellationToken);
        
        return isUserNameExist;
    }
}