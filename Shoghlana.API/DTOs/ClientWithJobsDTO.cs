using System.ComponentModel.DataAnnotations;

namespace Shoghlana.API.DTOs;

public class ClientWithJobsDTO
{
    public string Name { get; set; }

    public byte[]? Image { get; set; }

    public List<JobDTO>? Jobs { get; set; }
}
