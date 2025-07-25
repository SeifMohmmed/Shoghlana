namespace Shoghlana.Application.DTOs;

public class ClientWithJobsDTO
{
    public string Name { get; set; }

    public byte[]? Image { get; set; }

    public List<AddJobDTO>? Jobs { get; set; }
}
