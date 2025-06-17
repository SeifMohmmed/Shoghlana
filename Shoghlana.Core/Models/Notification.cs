using Shoghlana.Core.Enums;

namespace Shoghlana.Core.Models;
public class Notification
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public Client? Client { get; set; }

    public int? FreelancerId { get; set; }

    public Freelancer? Freelancer { get; set; }

    public string Title { get; set; }

    public DateTime SentTime { get; set; }

    public string? Description { get; set; }

    public NotificationReason Reason { get; set; }

    public int? NotificationTriggerId { get; set; }

    public bool IsRead { get; set; }

}
