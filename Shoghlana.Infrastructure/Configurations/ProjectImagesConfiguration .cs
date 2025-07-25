using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoghlana.Domain.Entities;

namespace Shoghlana.Infrastructure.Configurations;
internal class ProjectImagesConfiguration : IEntityTypeConfiguration<ProjectImages>
{
    public void Configure(EntityTypeBuilder<ProjectImages> builder)
    {
        builder.HasKey(pI => pI.Id);

        builder.HasOne(pI => pI.Project)
                 .WithMany(p => p.Images)
                 .HasForeignKey(pI => pI.ProjectId);


        builder.HasData(
          new ProposalImages { Id = 1, Image = new byte[] { 0x20, 0x21, 0x22, 0x23 }, ProposalId = 1 },
          new ProposalImages { Id = 2, Image = new byte[] { 0x20, 0x21, 0x22, 0x23 }, ProposalId = 2 }
      );
    }
}
