using Microsoft.AspNetCore.Http;

namespace Application.DTOs.RequestDTOs.User;

public class UpdateUserRequest
{
    public string FullName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public IFormFile? Avatar { get; set; }
}

public class AvatarUploadRequest
{
    public IFormFile File { get; set; } = null!;
}

public class CreateUserRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string Role { get; set; } = "Customer";
    public IFormFile? Avatar { get; set; }
}

