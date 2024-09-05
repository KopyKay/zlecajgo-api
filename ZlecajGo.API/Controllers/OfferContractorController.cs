using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.OfferContractors.Commands.ContractUserWithOffer;
using ZlecajGo.Application.OfferContractors.Commands.UpdateContractedOffer;
using ZlecajGo.Application.OfferContractors.Dtos;
using ZlecajGo.Application.OfferContractors.Queries.GetContractedOffer;
using ZlecajGo.Application.OfferContractors.Queries.GetContractedOffers;
using ZlecajGo.Domain.Constants;

namespace ZlecajGo.API.Controllers;

[ApiController]
[Route("api/offerContractors")]
[Authorize(Policy = PolicyNames.HasProfileCompleted)]
public class OfferContractorController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OfferContractorDto>))]
    public async Task<IActionResult> GetContractedOffers()
    {
        var contractedOffers = await mediator.Send(new GetContractedOffersQuery());
        return Ok(contractedOffers);
    }
    
    [HttpGet("{offerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OfferContractorDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetContractedOffer([FromRoute] Guid offerId)
    {
        var contractedOffer = await mediator.Send(new GetContractedOfferQuery(offerId));
        return Ok(contractedOffer);
    }
    
    [HttpPost("{offerId:guid}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ContractUserWithOffer([FromRoute] Guid offerId)
    {
        await mediator.Send(new ContractUserWithOfferCommand(offerId));
        return Created();
    }

    [HttpPatch("{offerId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateContractedOffer([FromRoute] Guid offerId, [FromBody] UpdateContractedOfferCommand command)
    {
        command.OfferId = offerId;
        await mediator.Send(command);
        return NoContent();
    }
}