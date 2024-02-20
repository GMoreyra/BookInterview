using Data.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Initialization;

/// <summary>
/// Provides extension methods for configuring data repositories.
/// This class is responsible for registering the necessary repositories to the provided service collection.
/// </summary>
public static class DataConfiguration
{
    /// <summary>
    /// Adds the necessary repositories to the provided service collection.
    /// </summary>
    /// <param name="services">The service collection to add the repositories to.</param>
    public static IServiceCollection RegisterData(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();

        return services;
    }
}
