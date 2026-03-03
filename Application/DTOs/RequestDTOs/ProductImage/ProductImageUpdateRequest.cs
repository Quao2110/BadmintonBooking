namespace Application.DTOs.RequestDTOs.ProductImage;

public class ProductImageUpdateRequest
{
    public string ImageUrl { get; set; } = string.Empty;
    public bool? IsThumbnail { get; set; }
}
