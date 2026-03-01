using Application.DTOs.RequestDTOs.ProductImage;
using Application.DTOs.ResponseDTOs.ProductImage;

namespace Application.Interfaces.IServices;

public interface IProductImageService
{
    Task<IEnumerable<ProductImageResponse>> GetAllAsync();
    Task<IEnumerable<ProductImageResponse>> GetByProductIdAsync(Guid productId);
    Task<ProductImageResponse> GetByIdAsync(Guid id);
    Task<ProductImageResponse> CreateAsync(ProductImageCreateRequest request);
    Task<ProductImageResponse> UpdateAsync(Guid id, ProductImageUpdateRequest request);
    Task DeleteAsync(Guid id);
}
