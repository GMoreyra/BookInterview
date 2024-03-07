namespace Tests;

using Data.Entities;
using global::Api.Contracts.CreateBook;
using global::Api.Contracts.GetBooks;
using global::Api.Contracts.UpdateBook;
using System.Collections.ObjectModel;
using System.Globalization;

public static class FakeData
{
    public static Collection<BookEntity> EntityBookMocks() =>
        [
            new() { Id = "B-1", Author = "Author 1", Title = "Title 1", Genre = "Genre 1", Price = 10, Description = "Test description 1", PublishDate = DateTime.Parse("2021-01-01", CultureInfo.InvariantCulture) },
            new() { Id = "B-2", Author = "Author 2", Title = "Title 2", Genre = "Genre 2", Price = 20, Description = "Test description 2", PublishDate = DateTime.Parse("2021-02-01", CultureInfo.InvariantCulture) },
            new() { Id = "B-3", Author = "Author 3", Title = "Title 3", Genre = "Genre 3", Price = 30, Description = "Test description 3", PublishDate = DateTime.Parse("2021-03-01", CultureInfo.InvariantCulture) },
        ];

    public static Collection<GetBooksResponse> GetBooksResponseMocks() =>
        [
            new("B-1", "Author 1", "Test description 1", "Title 1", "Genre 1", 10, DateTime.Parse("2021-01-01", CultureInfo.InvariantCulture)),
            new("B-2", "Author 2", "Test description 2", "Title 2", "Genre 2",  20, DateTime.Parse("2021-02-01", CultureInfo.InvariantCulture)),
            new("B-3", "Author 3", "Test description 3", "Title 3", "Genre 3",  30, DateTime.Parse("2021-03-01", CultureInfo.InvariantCulture)),
        ];

    public static Collection<CreateBookResponse> CreateBookResponseMocks() =>
        [
            new("B-1", "Author 1", "Test description 1", "Title 1", "Genre 1",  10, DateTime.Parse("2021-01-01", CultureInfo.InvariantCulture)),
            new("B-2", "Author 2", "Test description 2", "Title 2", "Genre 2",  20, DateTime.Parse("2021-02-01", CultureInfo.InvariantCulture)),
            new("B-3", "Author 3", "Test description 3", "Title 3", "Genre 3",  30, DateTime.Parse("2021-03-01", CultureInfo.InvariantCulture)),
        ];

    public static Collection<UpdateBookResponse> UpdateBookResponseMocks() =>
        [
            new("B-1", "Author 1",  "Test description 1" ,"Title 1", "Genre 1", 10, DateTime.Parse("2021-01-01", CultureInfo.InvariantCulture)),
            new("B-2", "Author 2",  "Test description 2" ,"Title 2", "Genre 2", 20, DateTime.Parse("2021-02-01", CultureInfo.InvariantCulture)),
            new("B-3", "Author 3",  "Test description 3" ,"Title 3", "Genre 3", 30, DateTime.Parse("2021-03-01", CultureInfo.InvariantCulture)),
        ];


    public static Collection<CreateBookRequest> CreateBookRequestMocks() =>
        [
            new("Author 1", "Test description 1", "Title 1", "Genre 1", "10", "2021-01-01"),
            new("Author 2", "Test description 2", "Title 2", "Genre 2", "20", "2021-02-01"),
            new("Author 3", "Test description 3", "Title 3", "Genre 3", "30", "2021-03-01"),
        ];

    public static Collection<UpdateBookRequest> UpdateBookRequestMocks() =>
        [
            new("Author 1", "Test description 1", "Title 1", "Genre 1", "10", "2021-01-01"),
            new("Author 2", "Test description 2", "Title 2", "Genre 2", "20", "2021-02-01"),
            new("Author 3", "Test description 3", "Title 3", "Genre 3", "30", "2021-03-01"),
        ];
}
