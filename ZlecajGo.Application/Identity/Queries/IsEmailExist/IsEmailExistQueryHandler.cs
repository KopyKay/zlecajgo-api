using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Users.Queries.GetUsers;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Identity.Queries.IsEmailExist;

public class IsEmailExistQueryHandler
(
    ILogger<GetUsersQueryHandler> logger,
    UserManager<User> userManager
)    
: IRequestHandler<IsEmailExistQuery, bool>
{
    public async Task<bool> Handle(IsEmailExistQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Checking if an email [{Email}] already exists", request.Email);
        
        var isEmailExists = await userManager.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email == request.Email, cancellationToken);
        
        return isEmailExists;
    }
}