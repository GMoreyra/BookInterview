namespace Data.Contexts;

using Data.DummyGenerator;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

/// <summary>
/// The context for accessing the database.
/// </summary>
public class BookContext : DbContext
{
    private readonly IConfiguration _configuration;

    public BookContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public virtual DbSet<BookEntity> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgresDB"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookEntity>().ToTable("Books");
        modelBuilder.Entity<BookEntity>().HasData(DummyBookEntityGenerator.GenerateDummyBooks());
    }
}
