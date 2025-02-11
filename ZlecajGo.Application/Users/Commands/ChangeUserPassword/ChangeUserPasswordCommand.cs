using MediatR;

namespace ZlecajGo.Application.Users.Commands.ChangeUserPassword;

public class ChangeUserPasswordCommand : IRequest<bool>
{
    public string NewPassword { get; set; } = null!;
    public string ConfirmNewPassword { get; set; } = null!;
    public string CurrentPassword { get; set; } = null!;
}