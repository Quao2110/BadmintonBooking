using Application.DTOs.RequestDTOs.Court;
using Application.DTOs.ResponseDTOs.Court;

namespace Application.Interfaces.IServices;

public interface ICourtService
{
    Task<IEnumerable<CourtResponse>> GetAllAsync();
    Task<CourtResponse?> GetByIdAsync(Guid id);
    Task<CourtResponse> CreateAsync(CourtCreateRequest request);
    Task<CourtResponse> UpdateAsync(Guid id, CourtUpdateRequest request);
    Task<bool> DeleteAsync(Guid id);
}
