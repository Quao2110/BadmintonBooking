namespace Application.DTOs.ResponseDTOs.User;

public class UserResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string Role { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime? CreatedAt { get; set; }
}
