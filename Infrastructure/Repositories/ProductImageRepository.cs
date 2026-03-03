using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
{
    private readonly BadmintonBooking_PRM393Context _context;

    public ProductImageRepository(BadmintonBooking_PRM393Context context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(Guid productId)
    {
        return await _context.ProductImages
            .Where(pi => pi.ProductId == productId)
            .ToListAsync();
    }
}
