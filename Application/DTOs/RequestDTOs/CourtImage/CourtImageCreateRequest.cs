namespace Application.DTOs.RequestDTOs.CourtImage;

public class CourtImageCreateRequest
{
    public Guid CourtId { get; set; }
    public string ImageUrl { get; set; } = null!;
}
