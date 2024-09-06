using FluentValidation;
using ZlecajGo.Domain.Entities;
using ZlecajGo.Domain.Repositories;

namespace ZlecajGo.Application.OfferContractors.Commands.UpdateContractedOffer;

public class UpdateContractedOfferCommandValidator : AbstractValidator<UpdateContractedOfferCommand>
{
    private readonly IEnumerable<Status> _availableStatuses;
    
    public UpdateContractedOfferCommandValidator(IStatusRepository statusRepository)
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