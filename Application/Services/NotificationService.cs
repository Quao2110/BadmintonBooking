using Application.DTOs.RequestDTOs.Notification;
using Application.DTOs.ResponseDTOs.Notification;
using Application.Interfaces.IServices;
using Application.Interfaces.IUnitOfWork;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class NotificationService : INotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NotificationResponse>> GetByUserIdAsync(Guid userId)
    {
        var notifications = await _unitOfWork.NotificationRepository.GetAllAsync();
        var userNotifications = notifications.Where(n => n.UserId == userId).OrderByDescending(n => n.CreatedAt);
        return _mapper.Map<IEnumerable<NotificationResponse>>(userNotifications);
    }

    public async Task MarkAsReadAsync(Guid id)
    {
        var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(id)
            ?? throw new Exception("Notification not found.");

        notification.IsRead = true;
        _unitOfWork.NotificationRepository.Update(notification);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CreateAsync(NotificationCreateRequest request)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Message = request.Message,
            Type = request.Type,
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.NotificationRepository.CreateAsync(notification);
        await _unitOfWork.SaveChangesAsync();
    }
}
