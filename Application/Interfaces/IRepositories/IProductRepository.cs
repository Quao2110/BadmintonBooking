using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetAllWithIncludesAsync();
    Task<Product?> GetByIdWithIncludesAsync(Guid id);
    Task<(IEnumerable<Product> Items, int TotalItems)> GetPagedAsync(string? search, Guid? categoryId, int page, int pageSize);
}
