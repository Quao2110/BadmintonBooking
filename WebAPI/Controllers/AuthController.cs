using Application.DTOs.RequestDTOs.Auth;
using Application.Interfaces.IServices;
using Application.DTOs.ApiResponseDTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _authService.LoginAsync(request);
                return Ok(ApiResponse.Success(result.Message ?? "Login successful.", result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message));
            }
        }

        [HttpPost("login/2fa/verify")]
        public async Task<IActionResult> Verify2FALogin([FromBody] Verify2FALoginRequest request)
        {
            try
            {
                var result = await _authService.Verify2FALoginAsync(request);
                return Ok(ApiResponse.Success(result.Message ?? "Identity verified successfully.", result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message));
            }
        }

        [HttpPost("register/initiate")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                await _authService.RegisterAsync(request);
                return Ok(ApiResponse.Success("Verification code sent to your email."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message));
            }
        }

        [HttpPost("register/verify")]
        public async Task<IActionResult> VerifyOtp([FromBody] OtpVerificationRequest request)
        {
            try
            {
                var result = await _authService.VerifyOtpAndRegisterAsync(request);
                return Ok(ApiResponse.Success(result.Message ?? "Account created successfully.", result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message));
            }
        }

        [HttpPost("login/google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            try
            {
                var result = await _authService.GoogleLoginAsync(request);
                return Ok(ApiResponse.Success(result.Message ?? "Google authentication successful.", result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message));
            }
        }

        [HttpPost("register/direct")]
        public async Task<IActionResult> RegisterNormal([FromBody] RegisterRequest request)
        {
            try
            {
                var result = await _authService.RegisterNormalAsync(request);
                return Ok(ApiResponse.Success(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse.Fail(ex.Message));
            }
        }
    }
}