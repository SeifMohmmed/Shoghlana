using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoghlana.Domain.Entities;
using Shoghlana.Domain.Enums;

namespace Shoghlana.Infrastructure.Configurations;
internal class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
{
    public void Configure(EntityTypeBuilder<Proposal> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Price)
              .HasColumnType("Money");

        builder.Property(p => p.Status)
              .HasDefaultValue(ProposalStatus.Waiting);

        builder.HasOne(p => p.Freelancer)
              .WithMany(f => f.Proposals)
              .HasForeignKey(p => p.FreelancerId);

        builder.HasOne(p => p.Job)
              .WithMany(j => j.Proposals)
              .HasForeignKey(p => p.JobId);




        builder.HasData(
            new Proposal { Id = 1, Price = 300, Status = ProposalStatus.Waiting, FreelancerId = 1, JobId = 1 },
            new Proposal { Id = 2, Price = 400, Status = ProposalStatus.Waiting, FreelancerId = 2, JobId = 2 }
        );
    }
}
