using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoghlana.Core.Models;

namespace Shoghlana.EF.Configurations;
internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Description).IsRequired(false).HasMaxLength(100);
        builder.Property(p => p.Link).IsRequired(false).HasMaxLength(150);

        builder.HasOne(p => p.Freelancer)
                 .WithMany(f => f.Portfolio)
                 .HasForeignKey(p => p.FreelancerId);

        builder.HasData(
          new Project { Id = 1, Title = "Project1", Description = "Description for Project1", FreelancerId = 1, Poster = new byte[] { 0x20, 0x21, 0x22, 0x23 } },
          new Project { Id = 2, Title = "Project2", Description = "Description for Project2", FreelancerId = 2, Poster = new byte[] { 0x20, 0x21, 0x22, 0x23 } }
      );

        #region Old Config
        //entity.HasMany(p => p.skills)Add commentMore actions
        //      .WithMany(s => s.projects)
        //      .UsingEntity<Dictionary<string, object>>("projectSkills",
        //    j => j.HasOne<Skill>()
        //          .WithMany()
        //          .HasForeignKey("SkillId"),
        //    j => j.HasOne<Project>()
        //          .WithMany()
        //          .HasForeignKey("ProjectId"));

        // map relation with skills >> M:M
        //entity.HasMany(p => p.skills)
        //      .WithMany(s => s.projects)
        //      .UsingEntity<Dictionary<string, object>>("projectSkills",
        //    j => j.HasOne<Skill>()
        //          .WithMany()
        //          .HasForeignKey("SkillId"),
        //    j => j.HasOne<Project>()
        //          .WithMany()
        //          .HasForeignKey("ProjectId"));
        #endregion

    }
}
