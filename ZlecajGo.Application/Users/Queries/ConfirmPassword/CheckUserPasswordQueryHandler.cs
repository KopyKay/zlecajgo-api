using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Users.Queries.GetUsers;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Users.Queries.ConfirmPassword;

public class CheckUserPasswordQueryHandler
(
    ILogger<GetUsersQueryHandler> logger,
    UserManager<User> userManager,
    IUserContext userContext
)    
: IRequestHandler<CheckUserPasswordQuery, bool>
{
    public async Task<bool> Handle(CheckUserPasswordQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("Checking if password is correct for {@UserEmail}", currentUser!.Email);
        
        var currentUser = userContext.GetCurrentUser()!;
        var user = await userManager.FindByIdAsync(currentUser.Id);
        
        var isPasswordCorrect = await userManager.CheckPasswordAsync(user, request.Password);
        
        return isPasswordCorrect;
    }
}