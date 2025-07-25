using System.ComponentModel.DataAnnotations;

namespace Shoghlana.Application.DTOs;
public class MessageDTO
{
    [Required]
    public string From { get; set; }

    public string To { get; set; }

    [Required]
    public string Content { get; set; }

}
