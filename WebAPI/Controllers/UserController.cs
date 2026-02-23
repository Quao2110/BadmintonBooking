using Application.DTOs.ApiResponseDTO;
using Application.DTOs.RequestDTOs.User;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/users")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateUserRequest request)
    {
        try
        {
            var result = await _userService.CreateAsync(request);
            return Ok(ApiResponse.Success("User created successfully by Admin.", result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse.Fail(ex.Message));
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _userService.GetAllAsync();
            return Ok(ApiResponse.Success("Users retrieved successfully.", result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse.Fail(ex.Message));
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var result = await _userService.GetByIdAsync(id);
            return Ok(ApiResponse.Success("User retrieved successfully.", result));
        }
        catch (Exception ex)
        {
            return NotFound(ApiResponse.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Update user info (fullName, phoneNumber, avatar).
    /// </summary>
    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] UpdateUserRequest request)
    {
        try
        {
            var result = await _userService.UpdateAsync(id, request);
            return Ok(ApiResponse.Success("User updated successfully.", result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Upload avatar for a user.
    /// </summary>
    [HttpPost("{id:guid}/avatar")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadAvatar([FromRoute] Guid id, [FromForm] AvatarUploadRequest request)
    {
        try
        {
            var avatarUrl = await _userService.UploadAvatarAsync(id, request.File);
            return Ok(ApiResponse.Success("Avatar uploaded successfully.", new { avatarUrl }));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Change user password.
    /// </summary>
    [HttpPatch("{id:guid}/password")]
    public async Task<IActionResult> ChangePassword([FromRoute] Guid id, [FromBody] ChangePasswordRequest request)
    {
        try
        {
            await _userService.ChangePasswordAsync(id, request);
            return Ok(ApiResponse.Success("Password updated successfully."));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse.Fail(ex.Message));
        }
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await _userService.DeleteAsync(id);
            return Ok(ApiResponse.Success("User deleted successfully."));
        }
        catch (Exception ex)
        {
            return NotFound(ApiResponse.Fail(ex.Message));
        }
    }
}
