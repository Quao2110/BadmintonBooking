using Application.DTOs.RequestDTOs.User;
using Application.DTOs.ResponseDTOs.User;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.IServices;

public interface IUserService
{
    Task<IEnumerable<UserResponse>> GetAllAsync();
    Task<UserResponse> GetByIdAsync(Guid id);
    Task<UserResponse> UpdateAsync(Guid id, UpdateUserRequest request);
    Task DeleteAsync(Guid id);
    Task ChangePasswordAsync(Guid id, ChangePasswordRequest request);
    Task<string> UploadAvatarAsync(Guid id, IFormFile file);
    Task<UserResponse> CreateAsync(CreateUserRequest request);
}

