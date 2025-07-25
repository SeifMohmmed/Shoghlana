using Shoghlana.Domain.Entities;
using Shoghlana.Domain.Repositories;
using Shoghlana.Infrastructure.Persistence;

namespace Shoghlana.Infrastructure.Repositories;
public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
{
    public NotificationRepository(ApplicationDbContext context) : base(context) { }

}
