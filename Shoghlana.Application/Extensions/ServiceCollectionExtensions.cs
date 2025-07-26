using Microsoft.Extensions.DependencyInjection;

namespace Shoghlana.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        // AutoMapper
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);


    }
}
