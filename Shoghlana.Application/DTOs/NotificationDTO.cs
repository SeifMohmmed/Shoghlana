namespace Shoghlana.Application.DTOs;

public class NotificationDTO
{
    public string Title { get; set; }

    public DateTime SentTime { get; set; }

    public string Description { get; set; }

    public string SenderName { get; set; }

    public byte[]? SenderImage { get; set; }

}
