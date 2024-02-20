using Application.DTOs;
using Data.Entities;

namespace Tests;

public static class FakeData
{
    public static List<BookEntity> GetFakeEntityBooks()
    {
        return
        [
            new() { Id = "B-1", Author = "Author 1", Title = "Title 1", Genre = "Genre 1", Price = 10, Description = "Test description 1", PublishDate = DateTime.Parse("2021-01-01") },
            new() { Id = "B-2", Author = "Author 2", Title = "Title 2", Genre = "Genre 2", Price = 20, Description = "Test description 2", PublishDate = DateTime.Parse("2021-02-01") },
            new() { Id = "B-3", Author = "Author 3", Title = "Title 3", Genre = "Genre 3", Price = 30, Description = "Test description 3", PublishDate = DateTime.Parse("2021-03-01") },
            new() { Id = "B-4", Author = "Author 4", Title = "Title 4", Genre = "Genre 4", Price = 40, Description = "Test description 4", PublishDate = DateTime.Parse("2021-04-01") },
            new() { Id = "B-5", Author = "Author 5", Title = "Title 5", Genre = "Genre 5", Price = 50, Description = "Test description 5", PublishDate = DateTime.Parse("2021-05-01") },
            new() { Id = "B-6", Author = "Author 6", Title = "Title 6", Genre = "Genre 6", Price = 60, Description = "Test description 6", PublishDate = DateTime.Parse("2021-06-01") },
            new() { Id = "B-7", Author = "Author 7", Title = "Title 7", Genre = "Genre 7", Price = 70, Description = "Test description 7", PublishDate = DateTime.Parse("2021-07-01") },
            new() { Id = "B-8", Author = "Author 8", Title = "Title 8", Genre = "Genre 8", Price = 80, Description = "Test description 8", PublishDate = DateTime.Parse("2021-08-01") },
            new() { Id = "B-9", Author = "Author 9", Title = "Title 9", Genre = "Genre 9", Price = 90, Description = "Test description 9", PublishDate = DateTime.Parse("2021-09-01") },
            new() { Id = "B-10", Author = "Author 10", Title = "Title 10", Genre = "Genre 10", Price = 100, Description = "Test description 10", PublishDate = DateTime.Parse("2021-10-01") }
        ];
    }

    public static List<BookDto> GetFakeDtoBooks()
    {
        return
        [
            new() { Author = "Author 1", Title = "Title 1", Genre = "Genre 1", Price = 10, Description = "Test description 1", PublishDate = DateTime.Parse("2021-01-01") },
            new() { Author = "Author 2", Title = "Title 2", Genre = "Genre 2", Price = 20, Description = "Test description 2", PublishDate = DateTime.Parse("2021-02-01") },
            new() { Author = "Author 3", Title = "Title 3", Genre = "Genre 3", Price = 30, Description = "Test description 3", PublishDate = DateTime.Parse("2021-03-01") },
            new() { Author = "Author 4", Title = "Title 4", Genre = "Genre 4", Price = 40, Description = "Test description 4", PublishDate = DateTime.Parse("2021-04-01") },
            new() { Author = "Author 5", Title = "Title 5", Genre = "Genre 5", Price = 50, Description = "Test description 5", PublishDate = DateTime.Parse("2021-05-01") },
            new() { Author = "Author 6", Title = "Title 6", Genre = "Genre 6", Price = 60, Description = "Test description 6", PublishDate = DateTime.Parse("2021-06-01") },
            new() { Author = "Author 7", Title = "Title 7", Genre = "Genre 7", Price = 70, Description = "Test description 7", PublishDate = DateTime.Parse("2021-07-01") },
            new() { Author = "Author 8", Title = "Title 8", Genre = "Genre 8", Price = 80, Description = "Test description 8", PublishDate = DateTime.Parse("2021-08-01") },
            new() { Author = "Author 9", Title = "Title 9", Genre = "Genre 9", Price = 90, Description = "Test description 9", PublishDate = DateTime.Parse("2021-09-01") },
            new() { Author = "Author 10", Title = "Title 10", Genre = "Genre 10", Price = 100, Description = "Test description 10", PublishDate = DateTime.Parse("2021-10-01") }
        ];
    }
}