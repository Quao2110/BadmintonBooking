using Application.DTOs.ResponseDTOs.ProductImage;

namespace Application.DTOs.ResponseDTOs.Product;

public class ProductResponse
{
    public Guid Id { get; set; }
    public Guid? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public int? StockQuantity { get; set; }
    public bool? IsActive { get; set; }
    public List<ProductImageResponse> ProductImages { get; set; } = new();
}
