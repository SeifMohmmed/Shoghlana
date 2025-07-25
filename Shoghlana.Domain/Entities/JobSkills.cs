using System.ComponentModel.DataAnnotations.Schema;

namespace Shoghlana.Domain.Entities;
public class JobSkills
{

    [ForeignKey("Job")]
    public int JobId { get; set; }

    public Job Job { get; set; }

    [ForeignKey("Skill")]
    public int SkillId { get; set; }

    public Skill Skill { get; set; }
}
