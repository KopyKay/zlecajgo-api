namespace ZlecajGo.Application.Users.Dtos;

public class UserDto
{
    public string Id { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public string Email { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? ProfilePictureUrl { get; set; }
}