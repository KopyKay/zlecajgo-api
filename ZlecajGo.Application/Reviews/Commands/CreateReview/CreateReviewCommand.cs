using System.Text.Json.Serialization;
using MediatR;

namespace ZlecajGo.Application.Reviews.Commands.CreateReview;

public class CreateReviewCommand : IRequest<bool>
{
    public string? ReviewerId { get; internal set; }
    public string? RevieweeId => RevieweeIdFromQuery;
    public byte Rating { get; set; }
    public string Comment { get; set; } = null!;

    [JsonIgnore] 
    public string? RevieweeIdFromQuery { get; set; }
}