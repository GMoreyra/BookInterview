using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.Contexts;

/// <summary>
/// The factory for creating a new instance of the <see cref="BookContext"/> class.
/// </summary>
public class BookContextFactory : IDesignTimeDbContextFactory<BookContext>
{
    /// <summary>
    /// Creates a new instance of the <see cref="BookContext"/> class.
    /// This method reads the configuration from the appsettings.json file, 
    /// builds the DbContext options using the connection string named "DefaultConnection", 
    /// and then creates a new instance of the BookContext with those options.
    /// </summary>
    /// <param name="args">An array of command-line arguments.</param>
    /// <returns>A new instance of the <see cref="BookContext"/> class.</returns>
    public BookContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<BookContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseSqlite(connectionString);

        return new BookContext(builder.Options);
    }
}
