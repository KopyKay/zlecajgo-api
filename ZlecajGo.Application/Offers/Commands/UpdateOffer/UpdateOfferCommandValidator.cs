using FluentValidation;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Offers.Commands.UpdateOffer;

public class UpdateOfferCommandValidator : AbstractValidator<UpdateOfferCommand>
{
    private readonly IEnumerable<Status> _availableStatuses;
    
    public UpdateOfferCommandValidator(IStatusRepository statusRepository)
    {
        _availableStatuses = statusRepository.GetStatusesAsync().Result;
        
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
        
        RuleFor(command => command.StatusId)
            .Must(StatusIdExists)
            .WithMessage("Invalid status id.");
    }
    
    private bool StatusIdExists(int? statusId)
    {
        if (statusId == null) return true;
            return _availableStatuses.Any(s => s.Id == statusId);
    }
}