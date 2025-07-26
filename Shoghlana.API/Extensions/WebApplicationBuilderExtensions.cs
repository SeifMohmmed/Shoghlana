using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Shoghlana.API.Services.Implementations;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Application.Helpers;
using Shoghlana.Domain.Entities;
using Shoghlana.Infrastructure.Configurations;
using Shoghlana.Infrastructure.Hubs;
using Shoghlana.Infrastructure.Persistence;
using System.Text;

namespace Shoghlana.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        // Controllers
        services.AddControllers();

        // JWT Authentication
        services.Configure<JWT>(configuration.GetSection("JWT"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(op =>
        {
            op.SaveToken = false;
            op.RequireHttpsMetadata = false;
            op.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidAudience = configuration["JWT:Audience"],
                ValidateIssuer = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
            };
        });

        // CORS
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            });
        });


        // Identity
        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


        services.Configure<IdentityOptions>(options =>
        {
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ " +
                "أبجدهوزحطيكلمنسعفصقرشتثخذضظغ";
            options.User.RequireUniqueEmail = true;
        });


        // Generic Service
        services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

        // Services (Business Logic)
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProposalService, ProposalService>();
        services.AddScoped<IProposalImageService, ProposalImageService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IFreelancerService, FreelancerService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IRateService, RateService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<INotificationService, NotificationService>();


        // External Services (Mail, etc.)
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddScoped<IMailService, MailService>();

        // Google Auth Configuration
        services.Configure<GoogleAuthConfig>(configuration.GetSection("Authentication:Google"));

        services.AddSignalR();
        services.AddSingleton<IDictionary<string, UserRoomConnection>>(opt =>
            new Dictionary<string, UserRoomConnection>());




        // Swagger/OpenAPI
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // SignalR Chat Services (Presentation Layer)
        services.AddSingleton<ChatServices>();

    }
}
