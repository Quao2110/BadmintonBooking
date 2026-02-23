namespace Application.DTOs.ResponseDTOs.Auth
{
    public class AuthResponse
    {
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; } = null!;
        public string? Role { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Message { get; set; }
    }
}
