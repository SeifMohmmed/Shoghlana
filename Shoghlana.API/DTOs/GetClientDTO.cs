using System.ComponentModel.DataAnnotations;

namespace Shoghlana.API.DTOs;

public class GetClientDTO
{
    [Required(ErrorMessage = ("Name is Required"))]
    [MinLength(3, ErrorMessage = ("Name Must Be at Least 3 Character"))]
    public string Name { get; set; }

    public string? Description { get; set; }

    public string? Phone { get; set; }

    public string? Country { get; set; }

    public byte[]? Image { get; set; }
}
