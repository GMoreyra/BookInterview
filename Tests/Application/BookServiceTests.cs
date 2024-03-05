﻿namespace Tests.Application;

using Data.Entities;
using Data.Interfaces;
using global::Api.Contracts.CreateBook;
using global::Application.Enums;
using global::Application.Services;
using Moq;
using System.Globalization;
using Xunit;

public class BookServiceTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _bookService = new BookService(_bookRepositoryMock.Object);
    }

    [Theory]
    [InlineData(BookFilterBy.Id, "B-1")]
    [InlineData(BookFilterBy.Author, "Author 1")]
    [InlineData(BookFilterBy.Title, "Title 1")]
    [InlineData(BookFilterBy.Genre, "Genre 1")]
    [InlineData(BookFilterBy.Description, "Test description 1")]
    [InlineData(BookFilterBy.Price, "10")]
    [InlineData(BookFilterBy.PublishDate, "2021-01-01")]
    public async Task GetBooks_ValidAttribute_ReturnsExpectedBooks(BookFilterBy attribute, string value)
    {
        // Arrange
        var expectedBookList = FakeData.EntityBookMocks();
        var getBookResponse = FakeData.GetBooksResponseMocks().Where(book =>
            (attribute == BookFilterBy.Id && book.Id == value) ||
            (attribute == BookFilterBy.Author && book.Author == value) ||
            (attribute == BookFilterBy.Title && book.Title == value) ||
            (attribute == BookFilterBy.Genre && book.Genre == value) ||
            (attribute == BookFilterBy.Description && book.Description == value) ||
            (attribute == BookFilterBy.Price && book.Price.ToString() == value) ||
            (attribute == BookFilterBy.PublishDate && book.PublishDate.ToString() == value)
        );

        _bookRepositoryMock.Setup(repo => repo.GetBooksById(value)).ReturnsAsync(expectedBookList.Where(x => x.Id == value));
        _bookRepositoryMock.Setup(repo => repo.GetBooksByAuthor(value)).ReturnsAsync(expectedBookList.Where(x => x.Author == value));
        _bookRepositoryMock.Setup(repo => repo.GetBooksByTitle(value)).ReturnsAsync(expectedBookList.Where(x => x.Title == value));
        _bookRepositoryMock.Setup(repo => repo.GetBooksByGenre(value)).ReturnsAsync(expectedBookList.Where(x => x.Genre == value));
        _bookRepositoryMock.Setup(repo => repo.GetBooksByDescription(value)).ReturnsAsync(expectedBookList.Where(x => x.Description == value));
        _bookRepositoryMock.Setup(repo => repo.GetBooksByPrice(value)).ReturnsAsync(expectedBookList.Where(x => x.Price.ToString() == value));
        _bookRepositoryMock.Setup(repo => repo.GetBooksByPublishDate(value)).ReturnsAsync(expectedBookList.Where(x => x.PublishDate.ToString() == value));

        // Act
        var result = await _bookService.GetBooks(attribute, value);

        // Assert
        Assert.Equal(getBookResponse, result);
    }

    [Fact]
    public async Task GetBooks_InvalidAttribute_ReturnsAllBooks()
    {
        // Arrange
        var expectedBooks = FakeData.EntityBookMocks();
        var getBookResponse = FakeData.GetBooksResponseMocks();

        _bookRepositoryMock.Setup(repo => repo.GetBooks()).ReturnsAsync(expectedBooks);

        // Act
        var result = await _bookService.GetBooks((BookFilterBy)int.MaxValue, null);

        // Assert
        Assert.Equal(getBookResponse, result);
    }

    [Fact]
    public async Task UpdateBook_ValidBook_ReturnsUpdatedBook()
    {
        // Arrange
        var entityBook = FakeData.EntityBookMocks()[0];
        var updateBookRequest = FakeData.UpdateBookRequestMocks()[0];
        var updateBookResponse = FakeData.UpdateBookResponseMocks()[0];

        _bookRepositoryMock.Setup(repo => repo.UpdateBook(It.IsAny<BookEntity>())).ReturnsAsync(entityBook);

        // Act
        var result = await _bookService.UpdateBook(string.Empty, updateBookRequest);

        // Assert
        Assert.Equal(updateBookResponse, result);
    }

    [Fact]
    public async Task CreateBook_ValidBook_ReturnsAddedBook()
    {
        // Arrange
        var addedBook = new BookEntity { Id = "B-11", Author = "Author 11", Title = "Title 11", Genre = "Genre 11", Price = 110, Description = "Test description 11", PublishDate = DateTime.Parse("2021-11-01", CultureInfo.InvariantCulture) };
        var createBookRequest = new CreateBookRequest("Author 11", "Test description 11", "Title 11", "Genre 11", 110, DateTime.Parse("2021-11-01", CultureInfo.InvariantCulture));
        var createBookResponse = new CreateBookResponse("B-11", "Author 11", "Test description 11", "Title 11", "Genre 11", 110, DateTime.Parse("2021-11-01", CultureInfo.InvariantCulture));

        _bookRepositoryMock.Setup(repo => repo.AddBook(It.IsAny<BookEntity>())).ReturnsAsync(addedBook);

        // Act
        var result = await _bookService.CreateBook(createBookRequest);

        // Assert
        Assert.Equal(createBookResponse, result);
    }
}
