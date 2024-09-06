using FluentValidation;

namespace ZlecajGo.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(command => command.FullName)
            .MinimumLength(3)
            .WithMessage("Name with surname must be at least 3 characters long.")
            .MaximumLength(100)
            .WithMessage("Name with surname must not exceed 100 characters.");
        
        RuleFor(command => command.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Today.AddYears(-18)))
            .WithMessage("You must be at least 18 years old to register.");

        RuleFor(command => command.PhoneNumber)
            .Matches(@"^\+48[4-9]\d{8}$")
            .WithMessage("Invalid phone number. Correct format: +48XXXXXXXXX. First digit after +48 must be between 4 and 9.");
        
        RuleFor(command => command.Email)
            .EmailAddress()
            .WithMessage("Invalid email address.");

        RuleFor(command => command.UserName)
            .MinimumLength(3)
            .WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(30)
            .WithMessage("Username must not exceed 30 characters.")
            .Matches(@"^\S*$")
            .WithMessage("Username must not contain white spaces.");
    }
}