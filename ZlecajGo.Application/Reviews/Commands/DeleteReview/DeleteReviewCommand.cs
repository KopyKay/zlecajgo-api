using System.Text.Json.Serialization;
using MediatR;

namespace ZlecajGo.Application.Reviews.Commands.DeleteReview;

public record DeleteReviewCommand(string RevieweeId) : IRequest;