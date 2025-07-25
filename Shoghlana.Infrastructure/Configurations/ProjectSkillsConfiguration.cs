using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoghlana.Domain.Entities;

namespace Shoghlana.Infrastructure.Configurations;
internal class ProjectSkillsConfiguration : IEntityTypeConfiguration<ProjectSkills>
{
    public void Configure(EntityTypeBuilder<ProjectSkills> builder)
    {
        builder.HasKey(e => new { e.SkillId, e.ProjectId });

        builder.HasOne(ps => ps.Project)
                  .WithMany(p => p.Skills)
                  .HasForeignKey(ps => ps.ProjectId);

        builder.HasOne(ps => ps.Skill)
                   .WithMany(s => s.Projects)
                   .HasForeignKey(ps => ps.SkillId);

    }
}
