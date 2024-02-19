using Data.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Extensions;

/// <summary>
/// Provides extension methods for configuring data repositories.
/// </summary>
public class DataConfigurationExtension
{
    /// <summary>
    /// Adds the necessary repositories to the provided service collection.
    /// </summary>
    /// <param name="services">The service collection to add the repositories to.</param>
    public static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
    }
}
