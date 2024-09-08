using FluentValidation;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.Offers.Commands.UpdateOfferStatus;

public class UpdateOfferStatusCommandValidator : AbstractValidator<UpdateOfferStatusCommand>
{
    private readonly IEnumerable<Status> _availableStatuses;
    
    public UpdateOfferStatusCommandValidator(IStatusRepository statusRepository)
    {
        _availableStatuses = statusRepository.GetStatusesAsync().Result;
        
        RuleFor(command => command.StatusId)
            .Must(StatusIdExists)
            .WithMessage("Invalid status identifier!");
    }
    
    private bool StatusIdExists(int statusId)
    {
        return _availableStatuses.Any(s => s.Id == statusId);
    }
}