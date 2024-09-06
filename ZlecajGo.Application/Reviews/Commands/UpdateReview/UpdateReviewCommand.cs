using System.Text.Json.Serialization;
using MediatR;

namespace ZlecajGo.Application.Reviews.Commands.UpdateReview;

public class UpdateReviewCommand : IRequest
{
    [JsonIgnore]
    public string ReviewerId { get; set; } = null!;
    [JsonIgnore]
    public string RevieweeId { get; set; } = null!;
    public byte Rating { get; set; }
    public string Comment { get; set; } = null!;
}