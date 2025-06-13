using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using Shoghlana.Core.Enums;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.EF.Configurations;
internal class JobConfiguraion : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(j => j.Id);

        builder.Property(j => j.Title)
               .HasMaxLength(100);

        builder.Property(j => j.Description)
               .HasMaxLength(1000);

        builder.Property(j => j.MinBudget)
           .HasColumnType("decimal(18,2)");

        builder.Property(e => e.MaxBudget)
                   .HasColumnType("decimal(18,2)");

        builder.Property(j => j.Status)
               .HasDefaultValue(JobStatus.Active);

        builder.HasOne(j => j.Client)
              .WithMany(c => c.Jobs)
              .HasForeignKey(j => j.ClientId);

        builder.HasOne(j => j.AcceptedFreelancer)
               .WithMany(f => f.WorkingHistory)
               .HasForeignKey(j => j.AcceptedFreelancerId);

        builder.HasOne(j => j.Category)
             .WithMany(c => c.Jobs)
             .HasForeignKey(j => j.CategoryId);

        builder.HasData(
                       new Job
                       {
                           Id = 1,
                           Title = "Professional and Unique Logo Design",
                           PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                           Description = "Professional design and artistic work",
                           MinBudget = 100,
                           MaxBudget = 500,
                           ExperienceLevel = ExperienceLevel.Beginner,
                           Status = JobStatus.Active,
                           ClientId = 1,
                           AcceptedFreelancerId = 1,
                           CategoryId = 1
                       },
                       new Job
                       {
                           Id = 2,
                           Title = "Social Media Advertising Poster Design",
                           PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                           Description = "Design and administrative artwork",
                           MinBudget = 200,
                           MaxBudget = 700,
                           ExperienceLevel = ExperienceLevel.Intermediate,
                           Status = JobStatus.Active,
                           ClientId = 2,
                           AcceptedFreelancerId = 2,
                           CategoryId = 1
                       },
                       new Job
                       {
                           Id = 3,
                           Title = "Professional Business Card Design for Printing",
                           PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                           Description = "Business card design",
                           MinBudget = 150,
                           MaxBudget = 600,
                           ExperienceLevel = ExperienceLevel.Professional,
                           Status = JobStatus.Active,
                           ClientId = 3,
                           AcceptedFreelancerId = 3,
                           CategoryId = 1
                       },
                       new Job
                       {
                           Id = 4,
                           Title = "Lifetime Free Control Panel Installation",
                           PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                           Description = "Website and application development",
                           MinBudget = 300,
                           MaxBudget = 800,
                           ExperienceLevel = ExperienceLevel.Intermediate,
                           Status = JobStatus.Active,
                           ClientId = 4,
                           AcceptedFreelancerId = 4,
                           CategoryId = 2
                       },
                       new Job
                       {
                           Id = 5,
                           Title = "Company Profile Website Design",
                           PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                           Description = "Website programming",
                           MinBudget = 200,
                           MaxBudget = 700,
                           ExperienceLevel = ExperienceLevel.Beginner,
                           Status = JobStatus.Active,
                           ClientId = 5,
                           AcceptedFreelancerId = 5,
                           CategoryId = 3
                       },
                       new Job
                       {
                           Id = 6,
                           Title = "Mobile App Development for iOS and Android",
                           PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                           Description = "Programming and design of mobile apps",
                           MinBudget = 1000,
                           MaxBudget = 3000,
                           ExperienceLevel = ExperienceLevel.Professional,
                           Status = JobStatus.Active,
                           ClientId = 6,
                           AcceptedFreelancerId = 6,
                           CategoryId = 4
                       },
                       new Job
                       {
                           Id = 7,
                           Title = "E-Commerce Website Design and Development",
                           PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                           Description = "Programming and design of online shopping websites",
                           MinBudget = 500,
                           MaxBudget = 1500,
                           ExperienceLevel = ExperienceLevel.Intermediate,
                           Status = JobStatus.Active,
                           ClientId = 7,
                           AcceptedFreelancerId = 7,
                           CategoryId = 3
                       },
                           new Job
                           {
                               Id = 8,
                               Title = "Social Media Advertising Campaign Management",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Marketing and advertising for companies and individuals",
                               MinBudget = 300,
                               MaxBudget = 1000,
                               ExperienceLevel = ExperienceLevel.Beginner,
                               Status = JobStatus.Active,
                               ClientId = 8,
                               AcceptedFreelancerId = 8,
                               CategoryId = 5
                           },
                           new Job
                           {
                               Id = 9,
                               Title = "Illustration Design for Children's Books",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Illustration and drawing arts",
                               MinBudget = 200,
                               MaxBudget = 600,
                               ExperienceLevel = ExperienceLevel.Intermediate,
                               Status = JobStatus.Active,
                               ClientId = 9,
                               AcceptedFreelancerId = 9,
                               CategoryId = 6
                           },
                           new Job
                           {
                               Id = 10,
                               Title = "Advertising Content Writing for Website",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Marketing and advertising content writing",
                               MinBudget = 100,
                               MaxBudget = 300,
                               ExperienceLevel = ExperienceLevel.Beginner,
                               Status = JobStatus.Active,
                               ClientId = 10,
                               AcceptedFreelancerId = 10,
                               CategoryId = 1
                           },
                           new Job
                           {
                               Id = 11,
                               Title = "Employee Management System Design and Programming",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Advanced administrative systems programming",
                               MinBudget = 500,
                               MaxBudget = 2000,
                               ExperienceLevel = ExperienceLevel.Professional,
                               Status = JobStatus.Active,
                               ClientId = 11,
                               CategoryId = 2
                           },
                           new Job
                           {
                               Id = 12,
                               Title = "Feasibility Study for Future Business Project",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Economic and financial analysis",
                               MinBudget = 1000,
                               MaxBudget = 5000,
                               ExperienceLevel = ExperienceLevel.Professional,
                               Status = JobStatus.Active,
                               ClientId = 12,
                               //FreelancerId = 12,
                               CategoryId = 3
                           },
                           new Job
                           {
                               Id = 13,
                               Title = "Online Programming Lessons for Beginners",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Educational and training courses",
                               MinBudget = 50,
                               MaxBudget = 200,
                               ExperienceLevel = ExperienceLevel.Beginner,
                               Status = JobStatus.Active,
                               ClientId = 13,
                               //FreelancerId = 13,
                               CategoryId = 4
                           },
                           new Job
                           {
                               Id = 14,
                               Title = "Promotional Print Design for Cultural Event",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Graphic design and advertising",
                               MinBudget = 150,
                               MaxBudget = 500,
                               ExperienceLevel = ExperienceLevel.Intermediate,
                               Status = JobStatus.Active,
                               ClientId = 14,
                               //FreelancerId = 14,
                               CategoryId = 5
                           },
                           new Job
                           {
                               Id = 15,
                               Title = "Translation of Scientific Articles from English to Arabic",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Translation and writing",
                               MinBudget = 200,
                               MaxBudget = 800,
                               ExperienceLevel = ExperienceLevel.Professional,
                               Status = JobStatus.Active,
                               ClientId = 15,
                               //FreelancerId = 15,
                               CategoryId = 6
                           },
                           new Job
                           {
                               Id = 16,
                               Title = "Mobile Video Game Design and Development",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Video game programming",
                               MinBudget = 1000,
                               MaxBudget = 5000,
                               ExperienceLevel = ExperienceLevel.Professional,
                               Status = JobStatus.Active,
                               ClientId = 16,
                               //FreelancerId = 16,
                               CategoryId = 1
                           },
                           new Job
                           {
                               Id = 17,
                               Title = "Online Educational Platform Design",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Design and development of e-learning platforms",
                               MinBudget = 500,
                               MaxBudget = 1500,
                               ExperienceLevel = ExperienceLevel.Intermediate,
                               Status = JobStatus.Active,
                               ClientId = 17,
                               //FreelancerId = 17,
                               CategoryId = 2
                           },
                           new Job
                           {
                               Id = 18,
                               Title = "Content Management for Tech Blog",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Content writing and editing",
                               MinBudget = 200,
                               MaxBudget = 700,
                               ExperienceLevel = ExperienceLevel.Intermediate,
                               Status = JobStatus.Active,
                               ClientId = 18,
                               //FreelancerId = 18,
                               CategoryId = 3
                           },
                           new Job
                           {
                               Id = 19,
                               Title = "CRM System Design and Development",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "CRM system programming and customization",
                               MinBudget = 800,
                               MaxBudget = 2500,
                               ExperienceLevel = ExperienceLevel.Professional,
                               Status = JobStatus.Active,
                               ClientId = 1,
                               //FreelancerId = 19,
                               CategoryId = 4
                           },
                           new Job
                           {
                               Id = 20,
                               Title = "Data Analysis and Strategic Report Preparation for Companies",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Data analysis and report preparation",
                               MinBudget = 300,
                               MaxBudget = 1000,
                               ExperienceLevel = ExperienceLevel.Intermediate,
                               Status = JobStatus.Active,
                               ClientId = 2,
                               //FreelancerId = 20,
                               CategoryId = 5
                           },
                           new Job
                           {
                               Id = 21,
                               Title = "Writing and Editing E-Books on AI",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Educational and research content writing",
                               MinBudget = 500,
                               MaxBudget = 1500,
                               ExperienceLevel = ExperienceLevel.Professional,
                               Status = JobStatus.Active,
                               ClientId = 3,
                               //FreelancerId = 21,
                               CategoryId = 6
                           },
                           new Job
                           {
                               Id = 22,
                               Title = "Educational Website Design and Development for Students",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Programming and design of educational websites",
                               MinBudget = 400,
                               MaxBudget = 1200,
                               ExperienceLevel = ExperienceLevel.Intermediate,
                               Status = JobStatus.Active,
                               ClientId = 4,
                               //FreelancerId = 22,
                               CategoryId = 1
                           },
                           new Job
                           {
                               Id = 23,
                               Title = "Online Event Booking Platform Design and Programming",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Design and programming of booking apps",
                               MinBudget = 600,
                               MaxBudget = 1800,
                               ExperienceLevel = ExperienceLevel.Intermediate,
                               Status = JobStatus.Active,
                               ClientId = 5,
                               //FreelancerId = 23,
                               CategoryId = 2
                           },
                           new Job
                           {
                               Id = 24,
                               Title = "Website Search Engine Optimization (SEO)",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Improving website search engine performance",
                               MinBudget = 200,
                               MaxBudget = 800,
                               ExperienceLevel = ExperienceLevel.Beginner,
                               Status = JobStatus.Active,
                               ClientId = 6,
                               //FreelancerId = 24,
                               CategoryId = 3
                           },
                           new Job
                           {
                               Id = 25,
                               Title = "Inventory and Sales Management System for Small Businesses",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Integrated management systems programming",
                               MinBudget = 700,
                               MaxBudget = 2500,
                               ExperienceLevel = ExperienceLevel.Professional,
                               Status = JobStatus.Active,
                               ClientId = 7,
                               //FreelancerId = 25,
                               CategoryId = 4
                           },
                           new Job
                           {
                               Id = 26,
                               Title = "Feasibility Study for a New Residential Project",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Economic and financial analysis for real estate projects",
                               MinBudget = 1500,
                               MaxBudget = 5000,
                               ExperienceLevel = ExperienceLevel.Professional,
                               Status = JobStatus.Active,
                               ClientId = 8,
                               //FreelancerId = 26,
                               CategoryId = 5
                           },
                           new Job
                           {
                               Id = 27,
                               Title = "Online Personal Assistant App Design and Development",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Personal assistant app programming",
                               MinBudget = 800,
                               MaxBudget = 3000,
                               ExperienceLevel = ExperienceLevel.Professional,
                               Status = JobStatus.Active,
                               ClientId = 9,
                               //FreelancerId = 27,
                               CategoryId = 6
                           },
                           new Job
                           {
                               Id = 28,
                               Title = "Create and Manage Online Fundraising Campaign",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Marketing and fundraising",
                               MinBudget = 400,
                               MaxBudget = 1500,
                               ExperienceLevel = ExperienceLevel.Intermediate,
                               Status = JobStatus.Active,
                               ClientId = 10,
                               //FreelancerId = 28,
                               CategoryId = 1
                           },
                           new Job
                           {
                               Id = 29,
                               Title = "Interactive Educational Platform for Teaching Mathematics",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Design and programming of interactive educational platforms",
                               MinBudget = 600,
                               MaxBudget = 2000,
                               ExperienceLevel = ExperienceLevel.Intermediate,
                               Status = JobStatus.Active,
                               ClientId = 11,
                               //FreelancerId = 29,
                               CategoryId = 2
                           },
                           new Job
                           {
                               Id = 30,
                               Title = "Educational Video Game Design for Children",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Educational game programming and design",
                               MinBudget = 300,
                               MaxBudget = 1200,
                               ExperienceLevel = ExperienceLevel.Beginner,
                               Status = JobStatus.Active,
                               ClientId = 12,
                               //FreelancerId = 30,
                               CategoryId = 3
                           },
                           new Job
                           {
                               Id = 31,
                               Title = "Research Report on Public Policy",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Policy analysis and report preparation",
                               MinBudget = 200,
                               MaxBudget = 700,
                               ExperienceLevel = ExperienceLevel.Beginner,
                               Status = JobStatus.Active,
                               ClientId = 13,
                               //FreelancerId = 31,
                               CategoryId = 4
                           },
                           new Job
                           {
                               Id = 32,
                               Title = "Blog Content Management System Design and Programming",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Content management systems programming and customization",
                               MinBudget = 400,
                               MaxBudget = 1500,
                               ExperienceLevel = ExperienceLevel.Intermediate,
                               Status = JobStatus.Active,
                               ClientId = 14,
                               //FreelancerId = 32,
                               CategoryId = 5
                           },
                           new Job
                           {
                               Id = 33,
                               Title = "Marketing Campaign for a New Product",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Product marketing and advertising",
                               MinBudget = 300,
                               MaxBudget = 1000,
                               ExperienceLevel = ExperienceLevel.Beginner,
                               Status = JobStatus.Active,
                               ClientId = 15,
                               //FreelancerId = 33,
                               CategoryId = 6
                           },
                           new Job
                           {
                               Id = 34,
                               Title = "Engineering Project Management System Design and Programming",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Project management system programming",
                               MinBudget = 600,
                               MaxBudget = 2500,
                               ExperienceLevel = ExperienceLevel.Professional,
                               Status = JobStatus.Active,
                               ClientId = 16,
                               //FreelancerId = 34,
                               CategoryId = 1
                           },
                           new Job
                           {
                               Id = 35,
                               Title = "Programming Language Learning App Design and Development",
                               PostTime = new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615),
                               Description = "Educational apps programming and design",
                               MinBudget = 500,
                               MaxBudget = 1800,
                               ExperienceLevel = ExperienceLevel.Intermediate,
                               Status = JobStatus.Active,
                               ClientId = 17,
                               //FreelancerId = 35,
                               CategoryId = 2
                           }


               );

    }
}
