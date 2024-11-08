using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Identity.Queries.IsPhoneNumberExist;

public class IsPhoneNumberExistQueryHandler 
(
    ILogger<IsPhoneNumberExistQueryHandler> logger,
    UserManager<User> userManager
)    
: IRequestHandler<IsPhoneNumberExistQuery, bool>
{
    public async Task<bool> Handle(IsPhoneNumberExistQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Checking if a phone number [{PhoneNumber}] already exists", request.PhoneNumber);
        
        var isPhoneNumberExist = await userManager.Users
            .AsNoTracking()
            .AnyAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken);
        
        return isPhoneNumberExist;
    }
}