using Microsoft.EntityFrameworkCore;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.EF;
public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
	{}

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

    public DbSet<Category> Categories { get; set; }

    public DbSet<Rate> Rates { get; set; }
}
