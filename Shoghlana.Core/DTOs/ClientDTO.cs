using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shoghlana.Core.DTOs;

public class ClientDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = ("Name is Required"))]
    [MinLength(3, ErrorMessage = ("Name Must Be at Least 3 Character"))]
    public string Name { get; set; }

    public string? Description { get; set; }

    public string? Phone { get; set; }

    public string? Country { get; set; }

    public IFormFile? Image { get; set; }

    public DateTime RegisterationTime { get; set; }

    public int JobsCount { get; set; }

    public int CompletedJobsCount { get; set; }

}
