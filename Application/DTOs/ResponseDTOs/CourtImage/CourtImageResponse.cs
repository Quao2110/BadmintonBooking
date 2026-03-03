namespace Application.DTOs.ResponseDTOs.CourtImage;

public class CourtImageResponse
{
    public Guid Id { get; set; }
    public Guid CourtId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
}
