using MediatR;
using ZlecajGo.Application.Reviews.Dtos;

namespace ZlecajGo.Application.Reviews.Queries.GetReceivedReviewFromUser;

public record GetReceivedReviewFromUserQuery(string UserId) : IRequest<ReviewDto>;