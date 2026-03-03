using Application.Interfaces.IRepositories;

namespace Application.Interfaces.IUnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IServiceRepository ServiceRepository { get; }
    IProductRepository ProductRepository { get; }
    IProductImageRepository ProductImageRepository { get; }
    Task<int> SaveChangesAsync();
    Task CommitAsync();
}
