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
internal class ClientNotificationsConfiguration : IEntityTypeConfiguration<ClientNotification>
{
    public void Configure(EntityTypeBuilder<ClientNotification> builder)
    {
        builder.HasKey(cn => cn.ClientId);

        builder.Property(cn => cn.Title).IsRequired().HasMaxLength(50);
        builder.Property(cn => cn.Description).IsRequired(false).HasMaxLength(100);

        builder.HasOne(cn => cn.Client)
               .WithMany(c => c.Notifications)
               .HasForeignKey(cn => cn.ClientId);


    }
}
