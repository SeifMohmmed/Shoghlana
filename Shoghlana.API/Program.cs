
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shoghlana.API.Helpers;
using Shoghlana.API.Services.Implementations;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.Helpers;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.EF;
using Shoghlana.EF.Hubs;
using Shoghlana.EF.Repositories;
using System.Text;

namespace Shoghlana.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            //});

            builder.Services.AddSignalR();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IClientRepository,ClientRepository>();
            builder.Services.AddScoped<IJobRepository,JobRepository>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(op => 
              {
                  op.SaveToken = false;
                  op.RequireHttpsMetadata = false;

                  op.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuerSigningKey= true,
                      ValidateAudience = true,
                      ValidAudience = builder.Configuration["JWT:Audience"],
                      ValidateIssuer=true,
                      ValidIssuer = builder.Configuration["JWT:Issuer"],
                      ValidateLifetime=true,
                      IssuerSigningKey = 
                      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                      
                  };
              });

            // Registering the Unit of work inside the application container.
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IClientNotificationRepository, ClientNotificationRepository>();

            builder.Services.AddScoped<IJobRepository, JobRepository>();
            builder.Services.AddScoped<IJobSkillsRepository, JobSkillsRepository>();

            builder.Services.AddScoped<IFreelancerRepository, FreelancerRepository>();
            builder.Services.AddScoped<IFreelancerSkillsRepository, FreelancerSkillsRepository>();
            builder.Services.AddScoped<IFreelancerNotificationRepository, FreelancerNotificationRepository>();

            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IProjectImagesRepository, ProjectImagesRepository>();
            builder.Services.AddScoped<IProjectSkillsRepository, ProjectSkillsRepository>();

            builder.Services.AddScoped<IProposalRepository, ProposalRepository>();
            builder.Services.AddScoped<IProposalImagesRepository, PropsalImageRepository>();

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            builder.Services.AddScoped<ISkillRepository, SkillRepository>();

            builder.Services.AddScoped<IRateRepository, RateRepository>();

            // Registering the Generic Repository inside the application container.
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Registering the Generic Service inside the application container.
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            // Registering the Services inside the application container.
            builder.Services.AddScoped<IProposalService, ProposalService>();
            builder.Services.AddScoped<IProposalImageService, ProposalImageService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IClientService, ClientService>();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddCors();

            //************************************************************************

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.MapHub<NotificationHub>("/notificationHub");


            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
