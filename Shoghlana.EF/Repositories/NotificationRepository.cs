using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.EF.Repositories;
public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
{
    public NotificationRepository(ApplicationDbContext context) : base(context) { }

}
