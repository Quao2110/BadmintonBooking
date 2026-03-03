namespace Application.DTOs.RequestDTOs.Notification;

public class NotificationCreateRequest
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public string Type { get; set; }
}
