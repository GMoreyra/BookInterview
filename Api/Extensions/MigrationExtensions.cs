
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;
/// <summary>
/// Contains extension methods for applying migrations.
/// </summary>
internal static class MigrationExtensions
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

        dbContext.Database.EnsureCreated();

        if (dbContext.Books.Any())
        {
            return;
        }

        dbContext.Database.Migrate();
    }
}
