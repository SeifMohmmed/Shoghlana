﻿using Microsoft.EntityFrameworkCore;
using Shoghlana.Core.Enums;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.EF;
public class ApplicationDbContext : DbContext
{
    public DbSet<Freelancer> Freelancers { get; set; }

    public DbSet<Client> Clients { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<Job> Jobs { get; set; }

    public DbSet<Skill> Skills { get; set; }

    public DbSet<Proposal> Proposals { get; set; }

    public DbSet<ProjectImages> ProjectImages { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Rate> Rates { get; set; }

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
            entity.HasMany(j => j.skills)
                  .WithMany(s => s.jobs)
                  .UsingEntity<Dictionary<string, object>>("jobSkills",

                     j => j.HasOne<Skill>()
                           .WithMany()
                           .HasForeignKey("SkillId"),
                     j => j.HasOne<Job>()
                           .WithMany()
                           .HasForeignKey("JobId"));
        });
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.HasOne(p => p.Freelancer)
                 .WithMany(f => f.Portfolio)
                 .HasForeignKey(p => p.FreelancerId);

            // map relation with skills >> M:M
            entity.HasMany(p => p.skills)
                  .WithMany(s => s.projects)
                  .UsingEntity<Dictionary<string, object>>("projectSkills",
                j => j.HasOne<Skill>()
                      .WithMany()
                      .HasForeignKey("SkillId"),
                j => j.HasOne<Project>()
                      .WithMany()
                      .HasForeignKey("ProjectId"));
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

        #region Initial Data

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
        #endregion


        base.OnModelCreating(modelBuilder);


    }
}