namespace Application.DTOs.ResponseDTOs.Service;

public class ServiceResponse
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Unit { get; set; }
    public int? StockQuantity { get; set; }
    public bool? IsActive { get; set; }
}
