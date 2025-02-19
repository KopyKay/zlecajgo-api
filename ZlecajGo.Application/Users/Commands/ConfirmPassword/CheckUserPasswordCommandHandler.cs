using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ZlecajGo.Application.Users.Queries.GetUsers;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Users.Commands.ConfirmPassword;

public class CheckUserPasswordCommandHandler
(
    ILogger<GetUsersQueryHandler> logger,
    UserManager<User> userManager,
    IUserContext userContext
)    
: IRequestHandler<CheckUserPasswordCommand, bool>
{
    public async Task<bool> Handle(CheckUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser()!;
        var user = await userManager.FindByIdAsync(currentUser.Id);

        logger.LogInformation("Checking if password is correct for {@UserEmail}", user!.Email);
        
        var isPasswordCorrect = await userManager.CheckPasswordAsync(user, request.Password);
        
        return isPasswordCorrect;
    }
}