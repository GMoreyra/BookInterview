using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

/// <summary>
/// Provides extension methods for configuring application services.
/// </summary>
public static class ApplicationConfigurationExtension
{
    /// <summary>
    /// Adds the application services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    public static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
    }
}
