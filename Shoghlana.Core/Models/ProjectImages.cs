using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoghlana.Core.Models;
public class ProjectImages
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Project")]
    public int? ProjectId { get; set; }

    public Project? Project { get; set; }

    public byte[] Image { get; set; }
}
