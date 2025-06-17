using Shoghlana.Core.Enums;
using Shoghlana.Core.Models;

namespace Shoghlana.Core.DTOs;
public class GetNotificationDTO
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int FreelancerId { get; set; }

    //public Client Client { get; set; }

    public string Title { get; set; }

    public DateTime SentTime { get; set; }

    public string Description { get; set; }

    public NotificationReason Reason { get; set; }  

    public int? NotificationTriggerId { get; set; }

}
