namespace ZlecajGo.Domain.Entities;

public class Review
{
    public User Reviewer { get; set; } = null!;
    public string ReviewerId { get; set; } = null!;
    
    public User Reviewee { get; set; } = null!;
    public string RevieweeId { get; set; } = null!;
    
    public byte Rating { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime PostDateTime { get; set; }
    public DateTime? UpdateDateTime { get; set; }
}