using Application.DTOs.ResponseDTOs.ProductImage;

namespace Application.DTOs.ResponseDTOs.Shop;

public class ShopResponse
{
    public Guid Id { get; set; }
    public string ShopName { get; set; } = null!;
    public string? Address { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public List<string> ImageUrls { get; set; } = new();
}
