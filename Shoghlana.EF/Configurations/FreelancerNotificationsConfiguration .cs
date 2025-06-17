//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.Extensions.Options;
//using Shoghlana.Core.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Shoghlana.EF.Configurations;
//internal class FreelancerNotificationsConfiguration : IEntityTypeConfiguration<FreelancerNotification>
//{
//    public void Configure(EntityTypeBuilder<FreelancerNotification> builder)
//    {
//        builder.HasKey(fn => fn.Id);

//        builder.Property(fn => fn.Title).IsRequired().HasMaxLength(50);
//        builder.Property(fn => fn.Description).IsRequired(false).HasMaxLength(2000);

//        builder.HasOne(fn => fn.Freelancer)
//                  .WithMany(f => f.Notifications)
//                  .HasForeignKey(fn => fn.FreelancerId);
//    }
//}
