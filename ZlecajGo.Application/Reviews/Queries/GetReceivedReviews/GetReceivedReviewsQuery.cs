using MediatR;
using ZlecajGo.Application.Reviews.Dtos;

namespace ZlecajGo.Application.Reviews.Queries.GetReceivedReviews;

public record GetReceivedReviewsQuery(string? UserId) : IRequest<IEnumerable<ReviewDto>>;