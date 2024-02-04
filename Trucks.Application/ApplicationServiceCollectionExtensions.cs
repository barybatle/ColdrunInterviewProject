using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Trucks.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ITrucksRepository, TrucksRepository>();
        services.AddScoped<ITruckService, TruckService>();
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();
        return services;
    }
}
