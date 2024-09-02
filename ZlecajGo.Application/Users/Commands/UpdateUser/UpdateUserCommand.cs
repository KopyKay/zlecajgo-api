using MediatR;

namespace ZlecajGo.Application.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest
{
    public string? FullName { get; set; } = null!;
    public DateOnly? BirthDate { get; set; }
    public string? Email { get; set; } = null!;
    public string? UserName { get; set; } = null!;
    public string? PhoneNumber { get; set; } = null!;
    public string? ProfilePictureUrl { get; set; }
    public bool? IsProfileCompleted { get; set; }
}