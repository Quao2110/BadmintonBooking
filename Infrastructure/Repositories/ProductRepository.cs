using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly BadmintonBooking_PRM393Context _context;

    public ProductRepository(BadmintonBooking_PRM393Context context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllWithIncludesAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdWithIncludesAsync(Guid id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<(IEnumerable<Product> Items, int TotalItems)> GetPagedAsync(
        string? search,
        Guid? categoryId,
        int page,
        int pageSize)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var trimmed = search.Trim();
            query = query.Where(p => p.ProductName.Contains(trimmed));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        var totalItems = await query.CountAsync();
        var items = await query
            .OrderBy(p => p.ProductName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalItems);
    }
}
