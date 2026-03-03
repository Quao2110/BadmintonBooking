namespace Application.DTOs.RequestDTOs.Shop;

public class ShopUpdateRequest
{
    public string ShopName { get; set; } = null!;
    public string? Address { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
