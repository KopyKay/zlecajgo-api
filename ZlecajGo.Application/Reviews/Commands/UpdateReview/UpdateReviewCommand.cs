using System.Text.Json.Serialization;
using MediatR;

namespace ZlecajGo.Application.Reviews.Commands.UpdateReview;

public class UpdateReviewCommand : IRequest
{
    public string? ReviewerId { get; internal set; }
    public string? RevieweeId => RevieweeIdFromQuery;
    public byte Rating { get; set; }
    public string Comment { get; set; } = null!;

    [JsonIgnore] 
    public string? RevieweeIdFromQuery { get; set; }
}