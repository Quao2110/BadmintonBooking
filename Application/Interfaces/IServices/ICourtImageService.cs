using Application.DTOs.RequestDTOs.CourtImage;
using Application.DTOs.ResponseDTOs.CourtImage;

namespace Application.Interfaces.IServices;

public interface ICourtImageService
{
    Task<CourtImageResponse> CreateAsync(CourtImageCreateRequest request);
    Task<bool> DeleteAsync(Guid id);
}
