using Data.DummyGenerator;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class BookContext : DbContext
{
    public virtual DbSet<BookEntity> Books { get; set; }

    public string DbPath { get; } = InitializeDbPath();

    public BookContext() { }

    public BookContext(DbContextOptions<BookContext> options) : base(options) { }

    private static string InitializeDbPath()
    {
        return Path.Join(Directory.GetCurrentDirectory(), "..", "Data", "books.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookEntity>().HasData(DummyBookEntityGenerator.GenerateDummyBooks());
    }
}
