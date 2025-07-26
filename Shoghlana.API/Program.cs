using Shoghlana.API.Extensions;
using Shoghlana.API.Services.Implementations;
using Shoghlana.Application.Extensions;
using Shoghlana.AspNetCore.SignalR.Hubs;
using Shoghlana.Infrastructure.Extensions;
using Shoghlana.Infrastructure.Hubs;
namespace Shoghlana.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.AddPresentation();

            builder.Services.AddApplication();

            builder.Services.AddInfrastructure(builder.Configuration);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();


            app.MapHub<NotificationHub>("/notificationHub");

            app.MapHub<ChatHub>("/ChatHub");
            app.MapHub<IndividualChatHub>("/IndividualChatHub");


            app.MapControllers();

            app.Run();
        }
    }
}
