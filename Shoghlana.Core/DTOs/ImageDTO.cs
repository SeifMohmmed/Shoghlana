using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shoghlana.Core.DTOs;
public class ImageDTO
{
    [Required(ErrorMessage = "Image is required")]

    public IFormFile? Image { get; set; }

    public int? ProjectId { get; set; }
}
