namespace ZlecajGo.Application.Users;

public record CurrentUser
(
    string Id,
    string Email,
    string UserName,
    string? FullName,
    string? PhoneNumber,
    DateOnly? BirthDate,
    bool IsProfileCompleted,
    IEnumerable<string> Roles
)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}