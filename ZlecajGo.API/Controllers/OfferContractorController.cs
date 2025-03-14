using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.OfferContractors.Commands.ContractUserWithOffer;
using ZlecajGo.Application.OfferContractors.Commands.UpdateContractedOffer;
using ZlecajGo.Application.OfferContractors.Dtos;
using ZlecajGo.Application.OfferContractors.Queries.GetContractedOfferOrOffers;
using ZlecajGo.Domain.Constants;

namespace ZlecajGo.API.Controllers;

[ApiController]
[Route("api/offerContractors")]
[Authorize(Policy = PolicyNames.HasProfileCompleted)]
public class OfferContractorController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OfferContractorDto))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OfferContractorDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetContractedOfferOrOffers([FromQuery] Guid? offerId)
    {
        var result = await mediator.Send(new GetContractedOfferOrOffersQuery(offerId));
        return result.Match<IActionResult>(Ok, Ok);
    }
    
    [HttpPost("createContract")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ContractUserWithOffer([FromBody, Required] ContractUserWithOfferCommand command)
    {
        var isCreated = await mediator.Send(command);
        return isCreated ? Created() : Conflict();
    }

    [HttpPatch("updateContract")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateContractedOffer([FromBody, Required] UpdateContractedOfferCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}