using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoghlana.Core.Models;
public class Client
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime RegisterationTime { get; set; }

    public string? Description { get; set; }

    public string? Phone { get; set; }

    public string? Country { get; set; }

    public byte[]? Image { get; set; }

    public List<Job>? Jobs { get; set; } = new List<Job>();

    [NotMapped]
    public int JobsCount => Jobs.Count;

    [NotMapped]
    public int CompletedJobsCount => Jobs.Where(j => j.Status == Enums.JobStatus.Closed).Count();

    public List<Notification>? Notifications { get; set; } = new List<Notification> { };

    public ApplicationUser? User { get; set; }
}
