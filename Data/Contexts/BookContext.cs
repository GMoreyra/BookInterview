namespace Data.Contexts;

using Data.DummyGenerator;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// The context for accessing the database.
/// </summary>
public class BookContext : DbContext
{
    /// <summary>
    /// The path to the database file.
    /// </summary>
    public string DbPath { get; } = InitializeDbPath();

    /// <summary>
    /// The set of books in the database.
    /// </summary>
    public virtual DbSet<BookEntity> Books { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BookContext"/> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    public BookContext(DbContextOptions<BookContext> options) : base(options) { }

    /// <summary>
    /// Initializes the path to the database file.
    /// </summary>
    /// <returns>The path to the database file.</returns>
    private static string InitializeDbPath() =>
        Path.Join(Directory.GetCurrentDirectory(), "..", "Data", "books.db");

    /// <summary>
    /// Configures the context for use with a SQLite database.
    /// </summary>
    /// <param name="options">The options builder to be used for configuration.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite($"Data Source={DbPath}");

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types
    /// exposed in <see cref="DbSet{TEntity}"/> properties on your derived context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<BookEntity>().HasData(DummyBookEntityGenerator.GenerateDummyBooks());
}
