using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoghlana.Core.Models;

namespace Shoghlana.EF.Configurations;
internal class JobSkillsConfiguration : IEntityTypeConfiguration<JobSkills>
{
    public void Configure(EntityTypeBuilder<JobSkills> builder)
    {

        builder.HasKey(js => new { js.JobId, js.SkillId });

        builder.HasOne(js => js.Job)
               .WithMany(j => j.Skills)
               .HasForeignKey(js => js.JobId);

        builder.HasOne(js => js.Skill)
            .WithMany()
            .HasForeignKey(js => js.SkillId);

    }
}
