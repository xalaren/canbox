using KanBox.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace KanBox.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
