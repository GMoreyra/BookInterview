namespace Api.Extensions;

using Data.Contexts;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Contains extension methods for applying migrations.
/// </summary>
public static class MigrationExtensions
{
    /// <summary>
    /// Applies any pending migrations for the context to the database.
    /// Will create the database if it does not already exist.
    /// </summary>
    /// <param name="app">The application builder instance.</param>
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using BookContext dbContext = scope.ServiceProvider.GetRequiredService<BookContext>();

        dbContext.Database.Migrate();
    }
}
