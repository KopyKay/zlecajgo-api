using FluentValidation;

namespace ZlecajGo.Application.Offers.Commands.UpdateOffer;

public class UpdateOfferCommandValidator : AbstractValidator<UpdateOfferCommand>
{
    public UpdateOfferCommandValidator()
    {
        RuleFor(command => command.Description)
            .Length(10, 500);
        
        RuleFor(command => command.Price)
            .GreaterThan(0).LessThan(100000.00m);
        
        RuleFor(command => command.City)
            .Length(2, 40);
        
        RuleFor(command => command.Street)
            .Length(2, 60);
        
        RuleFor(command => command.ZipCode)
            .Matches(@"^\d{2}-\d{3}$");
    }
}