using Application.DTOs.ResponseDTOs.CourtImage;

namespace Application.DTOs.ResponseDTOs.Court;

public class CourtResponse
{
    public Guid Id { get; set; }
    public string CourtName { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = null!;
    public List<CourtImageResponse> CourtImages { get; set; } = new();
}
