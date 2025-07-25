using System.ComponentModel.DataAnnotations;

namespace Shoghlana.Application.DTOs;
public class GetImageDTO
{
    [Required(ErrorMessage = "Image is required")]
    public byte[] Image { get; set; }

    public int? ProjectId { get; set; }
}
