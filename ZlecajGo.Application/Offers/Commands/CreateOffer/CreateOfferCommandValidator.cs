using FluentValidation;

namespace ZlecajGo.Application.Offers.Commands.CreateOffer;

public class CreateOfferCommandValidator : AbstractValidator<CreateOfferCommand>
{
    public CreateOfferCommandValidator()
    {
        RuleFor(command => command.Title)
            .Length(5, 70);
        
        RuleFor(command => command.Description)
            .Length(10, 500);
        
        RuleFor(command => command.Price)
            .GreaterThan(0).LessThan(100000.00m);
        
        RuleFor(command => command.ExpiryDateTime)
            .GreaterThan(DateTime.UtcNow).LessThan(DateTime.UtcNow.AddDays(7));
        
        RuleFor(command => command.City)
            .Length(2, 40);
        
        RuleFor(command => command.Street)
            .Length(2, 60);
        
        RuleFor(command => command.ZipCode)
            .Matches(@"^\d{2}-\d{3}$");
    }
}