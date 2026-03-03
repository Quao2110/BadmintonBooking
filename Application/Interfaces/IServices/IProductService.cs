using Application.DTOs.RequestDTOs.Product;
using Application.DTOs.ResponseDTOs.Common;
using Application.DTOs.ResponseDTOs.Product;

namespace Application.Interfaces.IServices;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllAsync();
    Task<ProductResponse> GetByIdAsync(Guid id);
    Task<ProductResponse> CreateAsync(ProductCreateRequest request);
    Task<ProductResponse> UpdateAsync(Guid id, ProductUpdateRequest request);
    Task DeleteAsync(Guid id);
    Task<PagedResult<ProductResponse>> GetPagedAsync(ProductListQuery query);
}
