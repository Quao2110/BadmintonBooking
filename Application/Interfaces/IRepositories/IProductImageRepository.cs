using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IProductImageRepository : IGenericRepository<ProductImage>
{
    Task<IEnumerable<ProductImage>> GetByProductIdAsync(Guid productId);
}
