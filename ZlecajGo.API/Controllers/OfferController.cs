using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Offers.Commands.CreateOffer;
using ZlecajGo.Application.Offers.Commands.DeleteOffer;
using ZlecajGo.Application.Offers.Commands.UpdateOffer;
using ZlecajGo.Application.Offers.Dtos;
using ZlecajGo.Application.Offers.Queries.GetOffer;
using ZlecajGo.Application.Offers.Queries.GetOffers;
using ZlecajGo.Domain.Constants;

namespace ZlecajGo.API.Controllers;

[ApiController]
[Route("api/offers")]
[Authorize(Policy = PolicyNames.HasProfileCompleted)]
public class OfferController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OfferDto>))]
    public async Task<IActionResult> GetOffers()
    {
        var offers = await mediator.Send(new GetOffersQuery());
        return Ok(offers);
    }

    [HttpGet("{offerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OfferDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOffer([FromRoute] Guid offerId)
    {
        var offer = await mediator.Send(new GetOfferQuery(offerId));
        return Ok(offer);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateOffer([FromBody] CreateOfferCommand command)
    {
        var offerId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetOffer), new { offerId }, null);
    }
    
    [HttpPatch("{offerId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOffer([FromRoute] Guid offerId, [FromBody] UpdateOfferCommand command)
    {
        command.OfferId = offerId;
        var isUpdated = await mediator.Send(command);

        if (isUpdated) return NoContent();
        
        return NotFound();
    }
    
    [HttpDelete("{offerId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOffer([FromRoute] Guid offerId)
    {
        var isDeleted = await mediator.Send(new DeleteOfferCommand(offerId));

        if (isDeleted) return NoContent();
        
        return NotFound();
    }
}