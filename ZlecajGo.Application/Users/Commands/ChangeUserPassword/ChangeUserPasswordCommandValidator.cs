using FluentValidation;

namespace ZlecajGo.Application.Users.Commands.ChangeUserPassword;

public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(command => command.NewPassword)
            .MinimumLength(6).WithMessage("Passwords must be at least 6 characters.")
            .Matches(@"[^\w\d]").WithMessage("Passwords must have at least one non alphanumeric character.")
            .Matches(@"\d").WithMessage("Passwords must have at least one digit ('0'-'9').")
            .Matches(@"[A-Z]").WithMessage("Passwords must have at least one uppercase ('A'-'Z').");
        
        RuleFor(command => command.ConfirmNewPassword)
            .Equal(command => command.NewPassword).WithMessage("Passwords must match.");
    }
}