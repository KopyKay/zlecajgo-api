using MediatR;
using ZlecajGo.Application.Reviews.Dtos;

namespace ZlecajGo.Application.Reviews.Queries.GetWrittenReviewForUser;

public record GetWrittenReviewForUserQuery(string UserId) : IRequest<ReviewDto>;