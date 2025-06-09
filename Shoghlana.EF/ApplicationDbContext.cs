﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shoghlana.Core.Enums;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public DbSet<FreelancerNotification> FreelancerNotifications { get; set; }

    public DbSet<ClientNotification> ClientNotifications { get; set; }

    public DbSet<Proposal> Proposals { get; set; }

    public DbSet<ProjectImages> ProjectImages { get; set; }

    public DbSet<ProposalImages> ProposalImages { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Rate> Rates { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    ///<summary>
    ///Enforce Validation on RunTime Before Sending Changes to DB 
    ///  This method will enforce validation on all entities, 
    ///  regardless of how they are added or modified, 
    ///  including those configured via Fluent API.
    /// </summary>
    public override int SaveChanges()
    {
        var Entities = from e in ChangeTracker.Entries()
                       where e.State == EntityState.Modified ||
                       e.State == EntityState.Added //&&( e.Entity is Employee)  => can use certain conditions also if needed
                       select e.Entity;

        foreach (var Entity in Entities)
        {
            ValidationContext validationContext = new ValidationContext(Entity);
            Validator.ValidateObject(Entity, validationContext, true);
            //true: This parameter specifies whether to validate all properties (when true) or only required properties (when false).
        }

        return base.SaveChanges();
    }

}