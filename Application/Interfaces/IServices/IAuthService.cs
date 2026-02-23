using Application.DTOs.RequestDTOs.Auth;
using Application.DTOs.ResponseDTOs.Auth;


namespace Application.Interfaces.IServices;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task RegisterAsync(RegisterRequest request);
    Task<AuthResponse> VerifyOtpAndRegisterAsync(OtpVerificationRequest request);
    Task<AuthResponse> GoogleLoginAsync(GoogleLoginRequest request);
    Task<string> RegisterNormalAsync(RegisterRequest request);
    Task<AuthResponse> Verify2FALoginAsync(Verify2FALoginRequest request);
}
