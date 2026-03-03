using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IShopRepository : IGenericRepository<Shop>
{
    Task<Shop?> GetFirstAsync();
}
