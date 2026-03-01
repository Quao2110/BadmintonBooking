namespace Application.DTOs.RequestDTOs.ProductImage;

public class ProductImageCreateRequest
{
    public Guid ProductId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool? IsThumbnail { get; set; }
}
