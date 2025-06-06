using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.DTOs;

public class NotificationDTO
{
    public string Title { get; set; }

    public DateTime SentTime { get; set; }

    public string Description { get; set; }

    public string SenderName { get; set; }

    public byte[]? SenderImage { get; set; }

}
