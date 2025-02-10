using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Exceptions;

namespace ZlecajGo.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler
(
    ILogger<UpdateUserCommandHandler> logger,
    UserManager<User> userManager,
    IUserContext userContext
)    
: IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser()!;
        
        logger.LogInformation("Updating user with id [{UserId}]", user.Id);
        
        var dbUser = await userManager.FindByIdAsync(user.Id)
            ?? throw new NotFoundException(nameof(User), user.Id);
        
        dbUser.FullName = request.FullName ?? dbUser.FullName;
        dbUser.BirthDate = request.BirthDate ?? dbUser.BirthDate;
        dbUser.Email = request.Email ?? dbUser.Email;
        dbUser.UserName = request.UserName ?? dbUser.UserName;
        dbUser.PhoneNumber = request.PhoneNumber ?? dbUser.PhoneNumber;
        dbUser.ProfilePictureUrl = request.ProfilePictureUrl ?? dbUser.ProfilePictureUrl;

        if (dbUser.IsProfileCompleted == false && dbUser is 
            { 
                FullName: not null, 
                BirthDate: not null, 
                Email: not null, 
                UserName: not null, 
                PhoneNumber: not null 
            })
        {
            dbUser.IsProfileCompleted = true;
        }
        
        await userManager.UpdateAsync(dbUser);
    }
}