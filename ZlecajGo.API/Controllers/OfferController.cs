using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Offers.Commands.CreateOffer;
using ZlecajGo.Application.Offers.Commands.DeleteOffer;
using ZlecajGo.Application.Offers.Commands.UpdateOffer;
using ZlecajGo.Application.Offers.Commands.UpdateOfferStatus;
using ZlecajGo.Application.Offers.Dtos;
using ZlecajGo.Application.Offers.Queries.GetCurrentUserOffers;
using ZlecajGo.Application.Offers.Queries.GetOfferOrOffers;
using ZlecajGo.Domain.Constants;

namespace ZlecajGo.API.Controllers;

[ApiController]
[Route("api/offers")]
[Authorize(Policy = PolicyNames.HasProfileCompleted)]
public class OfferController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OfferDto))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OfferDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOfferOrOffers([FromQuery] Guid? offerId)
    {
        var result = await mediator.Send(new GetOfferOrOffersQuery(offerId));
        return result.Match<IActionResult>(Ok, Ok);
    }

    [HttpGet("currentUserOffers")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OfferDto>))]
    public async Task<IActionResult> GetCurrentUserOffers()
    {
        var userOffers = await mediator.Send(new GetCurrentUserOffersQuery());
        return Ok(userOffers);
    }
    
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateOffer([FromBody, Required] CreateOfferCommand command)
    {
        var offerId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetOfferOrOffers), new { offerId }, null);
    }
    
    [HttpPatch("update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOffer([FromQuery, Required] Guid offerId, [FromBody, Required] UpdateOfferCommand command)
    {
        command.OfferId = offerId;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("updateStatus")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOfferStatus([FromQuery, Required] Guid offerId, [FromBody, Required] UpdateOfferStatusCommand command)
    {
        command.OfferId = offerId;
        await mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteOffer([FromQuery, Required] Guid offerId)
    {
        var isDeleted = await mediator.Send(new DeleteOfferCommand(offerId));
        return isDeleted ? NoContent() : Conflict();
    }
}