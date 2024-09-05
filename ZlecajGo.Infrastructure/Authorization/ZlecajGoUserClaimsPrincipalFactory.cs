using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ZlecajGo.Domain.Constants;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Infrastructure.Authorization;

public class ZlecajGoUserClaimsPrincipalFactory
(
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> options
)
: UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);
        
        if (user.UserName != null)
            id.AddClaim(new Claim(AppClaimTypes.UserName, user.UserName));
        
        if (user.FullName != null)
            id.AddClaim(new Claim(AppClaimTypes.FullName, user.FullName));
        
        if (user.PhoneNumber != null)
            id.AddClaim(new Claim(AppClaimTypes.PhoneNumber, user.PhoneNumber));
        
        if (user.BirthDate != null)
            id.AddClaim(new Claim(AppClaimTypes.BirthDate, user.BirthDate.Value.ToString("dd-MM-yyyy")));
        
        id.AddClaim(new Claim(AppClaimTypes.IsProfileCompleted, user.IsProfileCompleted.ToString()));

        return new ClaimsPrincipal(id);
    }
}