using System.ComponentModel.DataAnnotations;

namespace Shoghlana.Application.DTOs;
public class RateDTO
{
    public string? Feedback { get; set; }


    [Range(1, 5, ErrorMessage = "Value must be between 1 and 5")]
    public int? Value { get; set; }


    [Required(ErrorMessage = "JobId is required.")]
    public int JobId { get; set; }

}
