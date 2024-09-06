using MediatR;
using ZlecajGo.Application.Reviews.Dtos;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Reviews.Queries.GetWrittenReviews;

public record GetWrittenReviewsQuery(string? UserId) : IRequest<IEnumerable<ReviewDto>>;