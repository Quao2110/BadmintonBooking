using Application.DTOs.ResponseDTOs.Payment;

namespace Application.Interfaces.IServices;

public interface IPaymentService
{
    Task<CreatePaymentUrlResponse> CreateVnPayPaymentUrlAsync(Guid userId, Guid orderId, string? returnUrl, string? clientIp);
    Task<object> HandleVnPayIpnAsync(IReadOnlyDictionary<string, string> queryParams);
    Task<VnPayResultResponse> HandleVnPayReturnAsync(IReadOnlyDictionary<string, string> queryParams);
}
