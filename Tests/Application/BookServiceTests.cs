﻿using Application.DTOs;
using Application.Services;
using Data.Entities;
using Data.Interfaces;
using Moq;
using Xunit;
using static Application.Enums.BookAttributeEnum;

namespace Tests.Application;

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
    [InlineData(BookAttribute.Id, "B-1")]
    [InlineData(BookAttribute.Author, "Author 1")]
    [InlineData(BookAttribute.Title, "Title 1")]
    [InlineData(BookAttribute.Genre, "Genre 1")]
    [InlineData(BookAttribute.Description, "Test description 1")]
    [InlineData(BookAttribute.Price, "10")]
    [InlineData(BookAttribute.PublishDate, "2021-01-01")]
    public async Task GetBooks_ValidAttribute_ReturnsExpectedBooks(BookAttribute attribute, string value)
    {
        var expectedBooks = FakeData.GetFakeEntityBooks();
        var expectedBookList = new List<BookEntity> { expectedBooks[0] };

        _bookRepositoryMock.Setup(repo => repo.GetBooksById(value)).ReturnsAsync(expectedBookList);
        _bookRepositoryMock.Setup(repo => repo.GetBooksByAuthor(value)).ReturnsAsync(expectedBookList);
        _bookRepositoryMock.Setup(repo => repo.GetBooksByTitle(value)).ReturnsAsync(expectedBookList);
        _bookRepositoryMock.Setup(repo => repo.GetBooksByGenre(value)).ReturnsAsync(expectedBookList);
        _bookRepositoryMock.Setup(repo => repo.GetBooksByDescription(value)).ReturnsAsync(expectedBookList);
        _bookRepositoryMock.Setup(repo => repo.GetBooksByPrice(value)).ReturnsAsync(expectedBookList);
        _bookRepositoryMock.Setup(repo => repo.GetBooksByPublishDate(value)).ReturnsAsync(expectedBookList);

        var result = await _bookService.GetBooks(attribute, value);

        Assert.Equal(expectedBookList, result);
    }

    [Fact]
    public async Task GetBooks_InvalidAttribute_ReturnsAllBooks()
    {
        var expectedBooks = FakeData.GetFakeEntityBooks();

        _bookRepositoryMock.Setup(repo => repo.GetBooks()).ReturnsAsync(expectedBooks);

        var result = await _bookService.GetBooks((BookAttribute)int.MaxValue, null);

        Assert.Equal(expectedBooks, result);
    }

    [Fact]
    public async Task UpdateBook_ValidBook_ReturnsUpdatedBook()
    {
        var bookDto = FakeData.GetFakeDtoBooks()[0];
        var updatedBook = new BookEntity { Id = "B-1", Author = "Updated Author", Title = "Updated Title" };

        _bookRepositoryMock.Setup(repo => repo.UpdateBook(It.IsAny<BookEntity>())).ReturnsAsync(updatedBook);

        var result = await _bookService.UpdateBook(updatedBook.Id, bookDto);

        Assert.Equal(updatedBook, result);
    }

    [Fact]
    public async Task CreateBook_ValidBook_ReturnsAddedBook()
    {
        var addedBook = new BookEntity { Id = "B-11", Author = "Author 11", Title = "Title 11", Genre = "Genre 11", Price = 110, Description = "Test description 11", PublishDate = DateTime.Parse("2021-11-01") };
        var bookDto = new BookDto { Author = "Author 11", Title = "Title 11", Genre = "Genre 11", Price = 110, Description = "Test description 11", PublishDate = DateTime.Parse("2021-11-01") };

        _bookRepositoryMock.Setup(repo => repo.AddBook(It.IsAny<BookEntity>())).ReturnsAsync(addedBook);

        var result = await _bookService.CreateBook(bookDto);

        Assert.Equal(addedBook, result);
    }
}
