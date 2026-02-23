using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface IJwtService
{
    string GenerateToken(User user);
}
