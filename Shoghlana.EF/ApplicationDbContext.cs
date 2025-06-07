﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shoghlana.Core.Enums;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.EF;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Freelancer> Freelancers { get; set; }

    public DbSet<Client> Clients { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<Job> Jobs { get; set; }

    public DbSet<Skill> Skills { get; set; }

    public DbSet<FreelancerSkills> FreelancerSkills { get; set; }

    public DbSet<JobSkills> JobSkills { get; set; }

    public DbSet<ProjectSkills> ProjectSkills { get; set; }

    public DbSet<Proposal> Proposals { get; set; }

    public DbSet<ProjectImages> ProjectImages { get; set; }

    public DbSet<ProposalImages> ProposalImages { get; set; }

    public DbSet<Category> Category { get; set; }

    public DbSet<Rate> Rates { get; set; }

    public DbSet<FreelancerNotification> FreelancerNotifications { get; set; }

    public DbSet<ClientNotification> ClientNotifications { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name).HasMaxLength(50);

            entity.HasMany(c => c.Notifications)
                  .WithOne(n => n.Client)
                  .HasForeignKey(n => n.ClientId);
        });

        //modelBuilder.Entity<Freelancer>(entity =>
        //{
        //    entity.HasKey(f => f.Id);

        //    entity.Property(f => f.Name).HasMaxLength(50);


        //    entity.HasMany(f => f.Notifications)
        //          .WithOne(n => n.Freelancer)
        //          .HasForeignKey(n => n.FreelancerId);

            //entity.HasMany(f => f.Skills)
            //      .WithMany(s => s.)
            //      .UsingEntity<Dictionary<string, object>>("freelancerSkills",
            //    j => j.HasOne<Skill>()
            //          .WithMany()
            //          .HasForeignKey("SkillId"),
            //    j => j.HasOne<Freelancer>()
            //          .WithMany()
            //          .HasForeignKey("FreelancerId"));

            // map relation with skills >> M:M
            //entity.HasMany(f => f.Skills)
            //      .WithMany(s => s.freelancers)
            //      .UsingEntity<Dictionary<string, object>>("freelancerSkills",  // j 
            //    j => j.HasOne<Skill>()
            //          .WithMany()
            //          .HasForeignKey("SkillId"),
            //    j => j.HasOne<Freelancer>()
            //          .WithMany()
            //          .HasForeignKey("FreelancerId"));
        //});

        modelBuilder.Entity<FreelancerSkills>(entity =>
        {
            entity.HasKey(fs => new { fs.SkillId, fs.FreelancerId });

            entity.HasOne(fs => fs.Freelancer)
                  .WithMany(f => f.Skills)
                  .HasForeignKey(fs => fs.FreelancerId);

            entity.HasOne(fs => fs.Skill)
                   .WithMany(s => s.Freelancers)
                   .HasForeignKey(fs => fs.SkillId);
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(j => j.Id);

            entity.Property(j => j.MinBudget)
            .HasColumnType("Money");

            entity.Property(j => j.MaxBudget)
            .HasColumnType("Money");

            entity.HasOne(j => j.Client)
                  .WithMany(c => c.Jobs)
                  .HasForeignKey(j => j.ClientId);

            entity.Property(j => j.Status)
                  .HasDefaultValue(JobStatus.Active);

            entity.HasOne(j => j.Freelancer)
                  .WithMany(f => f.WorkingHistory)
                  .HasForeignKey(j => j.FreelancerId);

            entity.HasOne(j => j.Category)
                 .WithMany(c => c.Jobs)
                 .HasForeignKey(j => j.CategoryId);


            // Map relation with skills (M:M)
            //entity.HasMany(j => j.Skills)
            //      .WithMany(s => s.jobs)
            //      .UsingEntity<Dictionary<string, object>>("jobSkills",

            //         j => j.HasOne<Skill>()
            //               .WithMany()
            //               .HasForeignKey("SkillId"),
            //         j => j.HasOne<Job>()
            //               .WithMany()
            //               .HasForeignKey("JobId"));
        });

        modelBuilder.Entity<JobSkills>(entity =>
        {
            entity.HasKey(e => new { e.SkillId, e.JobId });

            entity.HasOne(js => js.Job)
                  .WithMany(j => j.Skills)
                  .HasForeignKey(js => js.JobId);

            entity.HasOne(js => js.Skill)
                   .WithMany(s => s.Jobs)
                   .HasForeignKey(js => js.SkillId);
        });
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.HasOne(p => p.Freelancer)
                 .WithMany(f => f.Portfolio)
                 .HasForeignKey(p => p.FreelancerId);

            // map relation with skills >> M:M
            //entity.HasMany(p => p.Skills)
            //      .WithMany(s => s.projects)
            //      .UsingEntity<Dictionary<string, object>>("projectSkills",
            //    j => j.HasOne<Skill>()
            //          .WithMany()
            //          .HasForeignKey("SkillId"),
            //    j => j.HasOne<Project>()
            //          .WithMany()
            //          .HasForeignKey("ProjectId"));
        });

        modelBuilder.Entity<ProjectSkills>(entity =>
        {
            entity.HasKey(e => new { e.SkillId, e.ProjectId });

            entity.HasOne(ps => ps.Project)
                  .WithMany(p => p.Skills)
                  .HasForeignKey(ps => ps.ProjectId);

            entity.HasOne(ps => ps.Skill)
                   .WithMany(s => s.Projects)
                   .HasForeignKey(ps => ps.SkillId);
        });

        modelBuilder.Entity<ProjectImages>(entity =>
        {
            entity.HasKey(pI => pI.Id);

            entity.HasOne(pI => pI.Project)
                 .WithMany(p => p.Images)
                 .HasForeignKey(pI => pI.ProjectId);
        });

        modelBuilder.Entity<Proposal>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Price)
                  .HasColumnType("Money");

            entity.Property(p => p.Status)
                  .HasDefaultValue(ProposalStatus.Waiting);

            entity.HasOne(p => p.Freelancer)
                  .WithMany(f => f.Proposals)
                  .HasForeignKey(p => p.FreelancerId);

            entity.HasOne(p => p.Job)
                  .WithMany(j => j.Proposals)
                  .HasForeignKey(p => p.JobId);
        });

        modelBuilder.Entity<IdentityRole>().HasData(

            new IdentityRole()
            {
                Id = "e65f1d2b-5f4f-4d92-b0e7-4e9644aadc70",
                Name = "Admin",
                NormalizedName = "Admin".ToUpper(),
                ConcurrencyStamp = "a213b132-9cc2-4e62-a7df-4bcb4532c77e"
            },

            new IdentityRole()
            {
                Id = "d8fe8f6e-c5f2-4b99-9081-9635f12d6e7a",
                Name = "Client",
                NormalizedName = "Client".ToUpper(),
                ConcurrencyStamp = "ec5f6c27-9ac8-4e5f-b304-3bc8945e1c93"
            },

            new IdentityRole()
            {
                Id = "3b1a0d3a-dc70-4f5d-870f-8c3f32b9c5e3",
                Name = "Freelancer",
                NormalizedName = "Freelancer".ToUpper(),
                ConcurrencyStamp = "e79c3a53-2e77-4cf3-a8cd-1b5b46b730bd"
            }
         );

        modelBuilder.Entity<Rate>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.HasCheckConstraint("CK_VALUE_RANGE", "[Value] BETWEEN 1 AND 5");   // why warning??

            entity.HasOne(r => r.Job)
                 .WithOne(j => j.Rate)
                 .HasForeignKey<Rate>(r => r.JobId);
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(c => c.Id);
        });

        modelBuilder.Entity<ClientNotification>(entity =>
        {
            entity.HasKey(cn => cn.ClientId);

            entity.HasOne(cn => cn.Client)
                  .WithMany(c => c.Notifications)
                  .HasForeignKey(cn => cn.ClientId);
        });

        modelBuilder.Entity<FreelancerNotification>(entity =>
        {
            entity.HasKey(fn => fn.Id);

            entity.HasOne(fn => fn.Freelancer)
                  .WithMany(f => f.Notifications)
                  .HasForeignKey(fn => fn.FreelancerId);
        });

        #region Initial Data

        modelBuilder.Entity<Job>().HasData(
            new Job
            {
                Id = 1,
                Title = "Job1",
                PostTime = new DateTime(2025, 5, 10, 16, 22, 37, 705),
                Description = "Description for Job1",
                MinBudget = 100,
                MaxBudget = 500,
                ExperienceLevel = ExperienceLevel.Beginner,
                Status = JobStatus.Active,
                ClientId = 1,
                FreelancerId = 1,
                CategoryId = 1
            },
            new Job
            {
                Id = 2,
                Title = "Job2",
                PostTime = new DateTime(2025, 5, 10, 16, 22, 37, 705),
                Description = "Description for Job2",
                MinBudget = 200,
                MaxBudget = 700,
                ExperienceLevel = ExperienceLevel.Intermediate,
                Status = JobStatus.Active,
                ClientId = 2,
                FreelancerId = 2,
                CategoryId = 2
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Title = "Category1" },
            new Category { Id = 2, Title = "Category2" }
        );

        modelBuilder.Entity<Proposal>().HasData(
            new Proposal { Id = 1, Price = 300, Status = ProposalStatus.Waiting, FreelancerId = 1, JobId = 1 },
            new Proposal { Id = 2, Price = 400, Status = ProposalStatus.Waiting, FreelancerId = 2, JobId = 2 }
        );

        modelBuilder.Entity<Rate>().HasData(
            new Rate { Id = 1, Value = 4, JobId = 1 },
            new Rate { Id = 2, Value = 5, JobId = 2 }
        );

        modelBuilder.Entity<Freelancer>().HasData
        (
            new Freelancer() { Id = 1, Name = "Ahmed Mohammed", Title = "Back-End Developer" },
            new Freelancer() { Id = 2, Name = "Ali Suleiman", Title = "Front-End Developer" },
            new Freelancer() { Id = 3, Name = "Wael Abdul Rahim", Title = "Back-End Developer" }
        );

        modelBuilder.Entity<Skill>().HasData
        (
          new List<Skill>()
          {
              new Skill() { Id = 1, Title = "C#" },
              new Skill() { Id = 2, Title = "LINQ" },
              new Skill() { Id = 3, Title = "EF" },
              new Skill() { Id = 4, Title = "OOP" },
              new Skill() { Id = 5, Title = "Agile" },
              new Skill() { Id = 6, Title = "Blazor" },
          }
        );

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole()
            {
                Id = "43e0e661-bc87-41cb-8865-d2942590f0bd",
                Name = "Admin",
                NormalizedName = "Admin".ToUpper(),
                ConcurrencyStamp = "f37db9e5-310f-43c7-911a-13175601cfbf"
            },
               new IdentityRole()
               {
                   Id = "d14bce5b-fb04-43e2-9f93-bdfbb430e78e",
                   Name = "Client",
                   NormalizedName = "Client".ToUpper(),
                   ConcurrencyStamp = "5b5fb572-a893-4e1d-860b-165dc0e5d5c8"
               },
                 new IdentityRole()
                 {
                     Id = "d5ed44ff-f5b6-4b70-b837-3c9a6a49bc91",
                     Name = "Freelancer",
                     NormalizedName = "Freelancer".ToUpper(),
                     ConcurrencyStamp = "175d71bb-bc5d-428f-ab98-9076b535c5f5"
                 }
            );
        #endregion


        base.OnModelCreating(modelBuilder);


    }
}