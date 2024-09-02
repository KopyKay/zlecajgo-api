using FluentValidation;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(command => command.FullName)
            .MinimumLength(3)
            .WithMessage("Imię i nazwisko nie może być krótsze niż 3 znaki.")
            .MaximumLength(100)
            .WithMessage("Imię i nazwisko nie może być dłuższe niż 100 znaków.");
        
        RuleFor(command => command.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Today.AddYears(-18)))
            .WithMessage("Aby się zarejestrować musisz mieć ukończone 18 lat.");

        RuleFor(command => command.PhoneNumber)
            .Matches(@"^\+48[4-9]\d{8}$")
            .WithMessage("Niepoprawny numer telefonu.");
        
        RuleFor(command => command.Email)
            .EmailAddress()
            .WithMessage("Niepoprawny adres email.");

        RuleFor(command => command.UserName)
            .MinimumLength(3)
            .WithMessage("Nazwa użytkownika nie może być krótsza niż 3 znaki.")
            .MaximumLength(30)
            .WithMessage("Nazwa użytkownika nie może być dłuższa niż 30 znaków.")
            .Matches(@"^\S*$")
            .WithMessage("Nazwa użytkownika nie może zawierać spacji.");
    }
}