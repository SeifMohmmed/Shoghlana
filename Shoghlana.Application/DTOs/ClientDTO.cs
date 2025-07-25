using Microsoft.AspNetCore.Http;

namespace Shoghlana.Application.DTOs;

public class ClientDTO
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Phone { get; set; }

    public string? Country { get; set; }

    public IFormFile? Image { get; set; }

    public DateTime? RegisterationTime { get; set; } = DateTime.Now;

    public int? JobsCount { get; set; }

}
