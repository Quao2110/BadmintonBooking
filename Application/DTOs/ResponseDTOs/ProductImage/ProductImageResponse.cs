namespace Application.DTOs.ResponseDTOs.ProductImage;

public class ProductImageResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool? IsThumbnail { get; set; }
    public DateTime? CreatedAt { get; set; }
}
