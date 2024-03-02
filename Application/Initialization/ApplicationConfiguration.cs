namespace Application.Initialization;

using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for configuring application services.
/// </summary>
public static class ApplicationConfiguration
{
    /// <summary>
    /// Adds the application services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();

        return services;
    }
}
