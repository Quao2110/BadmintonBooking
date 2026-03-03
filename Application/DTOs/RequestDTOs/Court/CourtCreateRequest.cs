namespace Application.DTOs.RequestDTOs.Court;

public class CourtCreateRequest
{
    public string CourtName { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = "Active";
}
