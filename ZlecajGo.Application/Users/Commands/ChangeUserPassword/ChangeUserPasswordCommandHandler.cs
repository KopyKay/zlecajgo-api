using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Users.Commands.ChangeUserPassword;

public class ChangeUserPasswordCommandHandler 
(
    ILogger<ChangeUserPasswordCommand> logger,
    UserManager<User> userManager,
    IUserContext userContext
)      
: IRequestHandler<ChangeUserPasswordCommand, bool>
{
    public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser()!;
        var user = (await userManager.FindByIdAsync(currentUser.Id))!;
        
        logger.LogInformation("Changing password for {@UserEmail}", user.Email);
        
        var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        
        return result.Succeeded;
    }
}