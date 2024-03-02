namespace Data.Initialization;

using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

/// <summary>
/// Provides extension methods for configuring data repositories.
/// This class is responsible for registering the necessary repositories to the provided service collection.
/// </summary>
public static class DataConfiguration
{
    private const string DefaultConnection = "DefaultConnection";

    /// <summary>
    /// Adds the necessary repositories to the provided service collection.
    /// </summary>
    /// <param name="services">The service collection to add the repositories to.</param>
    public static IServiceCollection RegisterData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBookRepository, BookRepository>();

        services.AddScoped<DbConnection>(provider => new SqliteConnection(configuration.GetConnectionString(DefaultConnection)));
        services.AddDbContext<BookContext>(options => options.UseSqlite(configuration.GetConnectionString(DefaultConnection)));

        return services;
    }

}
