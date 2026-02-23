using Application.Interfaces.IRepositories;

namespace Application.Interfaces.IUnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    Task<int> SaveChangesAsync();
    Task CommitAsync();
}
