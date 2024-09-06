using MediatR;
using ZlecajGo.Application.Reviews.Dtos;

namespace ZlecajGo.Application.Reviews.Queries.GetReviews;

public class GetReviewsQuery : IRequest<IEnumerable<ReviewDto>>;