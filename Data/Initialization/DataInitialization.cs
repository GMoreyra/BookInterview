
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data.Common;

namespace Data.Initialization;
/// <summary>
/// Provides extension methods for configuring data repositories.
/// This class is responsible for registering the necessary repositories to the provided service collection.
/// </summary>
public static class DataInitialization
{
    private const string PostgresDbConnection = "PostgresDB";

    /// <summary>
    /// Adds the necessary repositories to the provided service collection.
    /// </summary>
    /// <param name="services">The service collection to add the repositories to.</param>
    public static IServiceCollection RegisterData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBookRepository, BookRepository>();

        services.AddScoped<DbConnection>(provider => new NpgsqlConnection(configuration.GetConnectionString(PostgresDbConnection)));
        services.AddDbContext<BookContext>(options => options.UseNpgsql(configuration.GetConnectionString(PostgresDbConnection)));

        return services;
    }

}
