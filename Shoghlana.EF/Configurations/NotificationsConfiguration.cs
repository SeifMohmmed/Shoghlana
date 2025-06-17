using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.EF.Configurations;
internal class NotificationsConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.Id); 

            builder.Property(n => n.Title).IsRequired().HasMaxLength(50);
        builder.Property(n => n.Description).IsRequired(false).HasMaxLength(2000);

        builder.HasOne(n => n.Client)
                  .WithMany(c => c.Notifications)
                  .HasForeignKey(c => c.ClientId);


        builder.HasOne(c => c.Freelancer)
                  .WithMany(c => c.Notifications)
                  .HasForeignKey(c => c.FreelancerId);
    }
}
