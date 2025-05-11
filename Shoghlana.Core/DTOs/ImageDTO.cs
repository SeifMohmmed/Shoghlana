using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.DTOs;
public class ImageDTO
{
    [Required(ErrorMessage = "Image is required")]

    public IFormFile? Image { get; set; }

    public int? ProjectId { get; set; }
}
