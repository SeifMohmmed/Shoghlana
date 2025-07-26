using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shoghlana.Domain.Repositories;
using Shoghlana.EF.Repositories;
using Shoghlana.Infrastructure.Persistence;
using Shoghlana.Infrastructure.Repositories;

namespace Shoghlana.Infrastructure.Extensions;
public static class ServiceCollectionExtentions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database Context
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Generic Repository
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        // Specific Repositories
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IJobSkillsRepository, JobSkillsRepository>();
        services.AddScoped<IFreelancerRepository, FreelancerRepository>();
        services.AddScoped<IFreelancerSkillsRepository, FreelancerSkillsRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectImagesRepository, ProjectImagesRepository>();
        services.AddScoped<IProjectSkillsRepository, ProjectSkillsRepository>();
        services.AddScoped<IProposalRepository, ProposalRepository>();
        services.AddScoped<IProposalImagesRepository, PropsalImageRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<IRateRepository, RateRepository>();
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();


    }
}