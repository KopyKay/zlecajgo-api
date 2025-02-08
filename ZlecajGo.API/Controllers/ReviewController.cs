using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZlecajGo.Application.Reviews.Commands.CreateReview;
using ZlecajGo.Application.Reviews.Commands.DeleteReview;
using ZlecajGo.Application.Reviews.Commands.UpdateReview;
using ZlecajGo.Application.Reviews.Dtos;
using ZlecajGo.Application.Reviews.Queries.GetReceivedReviewFromUser;
using ZlecajGo.Application.Reviews.Queries.GetReceivedReviews;
using ZlecajGo.Application.Reviews.Queries.GetReviews;
using ZlecajGo.Application.Reviews.Queries.GetWrittenReviewForUser;
using ZlecajGo.Application.Reviews.Queries.GetWrittenReviews;
using ZlecajGo.Domain.Constants;

namespace ZlecajGo.API.Controllers;

[ApiController]
[Route("api/reviews")]
[Authorize(Policy = PolicyNames.HasProfileCompleted)]
public class ReviewController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDto>))]
    public async Task<IActionResult> GetReviews()
    {
        var reviews = await mediator.Send(new GetReviewsQuery());
        return Ok(reviews);
    }
    
    [HttpGet("received")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDto>))]
    public async Task<IActionResult> GetReceivedReviews([FromQuery] string? userId)
    {
        var reviews = await mediator.Send(new GetReceivedReviewsQuery(userId));
        return Ok(reviews);
    }
    
    [HttpGet("written")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReviewDto>))]
    public async Task<IActionResult> GetWrittenReviews([FromQuery] string? userId)
    {
        var reviews = await mediator.Send(new GetWrittenReviewsQuery(userId));
        return Ok(reviews);
    }
    
    [HttpGet("receivedFromUser/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReceivedReviewFromUser([FromRoute] string userId)
    {
        var review = await mediator.Send(new GetReceivedReviewFromUserQuery(userId));
        return Ok(review);
    }
    
    [HttpGet("writtenForUser/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReviewDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWrittenReviewForUser([FromRoute] string userId)
    {
        var review = await mediator.Send(new GetWrittenReviewForUserQuery(userId));
        return Ok(review);
    }

    [HttpPost("create/{userId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateReview([FromRoute] string userId, [FromBody] CreateReviewCommand command)
    {
        command.RevieweeId = userId;
        var result = await mediator.Send(command);
        return result ? Created() : BadRequest();
    }
    
    [HttpPatch("update/{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateReview([FromRoute] string userId, [FromBody] UpdateReviewCommand command)
    {
        command.RevieweeId = userId;
        await mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete("delete/{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReview([FromRoute] string userId)
    {
        await mediator.Send(new DeleteReviewCommand(userId));
        return NoContent();
    }
}