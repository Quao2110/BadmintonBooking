namespace Application.DTOs.RequestDTOs.Court;

public class CourtUpdateRequest
{
    public string CourtName { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = "Active";
}
