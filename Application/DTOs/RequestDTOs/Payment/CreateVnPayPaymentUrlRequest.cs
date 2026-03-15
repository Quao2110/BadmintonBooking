namespace Application.DTOs.RequestDTOs.Payment;

public class CreateVnPayPaymentUrlRequest
{
    public Guid OrderId { get; set; }

    // Optional custom deep link / frontend return URL. Falls back to configured default if omitted.
    public string? ReturnUrl { get; set; }
}
