using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ZlecajGo.Domain.Constants;

namespace ZlecajGo.Application.Users;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user is null)
            throw new InvalidOperationException("User context is not present!");
        
        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return null;
        
        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var userName = user.FindFirst(c => c.Type == AppClaimTypes.UserName)!.Value;
        var fullName = user.FindFirst(c => c.Type == AppClaimTypes.FullName)?.Value;
        var phoneNumber = user.FindFirst(c => c.Type == AppClaimTypes.PhoneNumber)?.Value;
        var birthDateString = user.FindFirst(c => c.Type == AppClaimTypes.BirthDate)?.Value;
        var birthDate = birthDateString is null ? (DateOnly?)null : DateOnly.ParseExact(birthDateString, "dd-MM-yyyy");
        var isProfileCompleted = bool.Parse(user.FindFirst(c => c.Type == AppClaimTypes.IsProfileCompleted)!.Value);
        
        return new CurrentUser(userId, email, userName, fullName, phoneNumber, birthDate, isProfileCompleted);
    }
}