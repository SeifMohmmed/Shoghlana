using Shoghlana.Domain.Enums;

namespace Shoghlana.Application.DTOs;
public class GetNotificationDTO
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int FreelancerId { get; set; }

    public string Title { get; set; }

    public DateTime SentTime { get; set; }

    public string Description { get; set; }

    public NotificationReason Reason { get; set; }

    public int? NotificationTriggerId { get; set; }

    public bool IsRead { get; set; }


}
