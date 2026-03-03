namespace Application.DTOs.RequestDTOs.Service;

public class ServiceCreateRequest
{
    public string ServiceName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Unit { get; set; }
    public int? StockQuantity { get; set; }
    public bool? IsActive { get; set; }
}
