using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.DbContexts;

namespace Infrastructure.Repositories;

public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
{
    private readonly BadmintonBooking_PRM393Context _context;

    public NotificationRepository(BadmintonBooking_PRM393Context context) : base(context)
    {
        _context = context;
    }
}
