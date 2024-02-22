using Api.Controllers;
using Application.DTOs;
using Application.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using static Application.Enums.BookAttributeEnum;

namespace Tests.Api;

public class BooksControllerTests
{
    private readonly Mock<IBookService> _bookServiceMock;
    private readonly BooksController _booksController;

    public BooksControllerTests()
    {
        _bookServiceMock = new Mock<IBookService>();

        _booksController = new BooksController(_bookServiceMock.Object);
    }

    [Fact]
    public async Task GetBooks_ReturnsAllBooks()
    {
        var expectedBooks = FakeData.GetFakeEntityBooks();
        _bookServiceMock.Setup(service => service.GetBooks(BookAttribute.None, null)).ReturnsAsync(expectedBooks);

        var result = await _booksController.GetBooks();

        var okResult = result.Result as OkObjectResult;

        Assert.NotNull(okResult);
        Assert.Equal(expectedBooks, okResult?.Value);
    }

    [Fact]
    public async Task UpdateBook_ValidBook_ReturnsUpdatedBook()
    {
        var bookToUpdate = FakeData.GetFakeEntityBooks()[0];
        var bookDto = FakeData.GetFakeDtoBooks()[0];

        var updatedBook = new BookEntity { Id = "B-1", Author = "Updated Author", Title = "Updated Title" };
        _bookServiceMock.Setup(service => service.UpdateBook(It.IsAny<string>(), It.IsAny<BookDto>())).ReturnsAsync(updatedBook);

        var result = await _booksController.UpdateBook(bookToUpdate.Id, bookDto);

        var okResult = result.Result as OkObjectResult;

        Assert.NotNull(okResult);
        Assert.Equal(updatedBook, okResult?.Value);
    }

    [Fact]
    public async Task AddBook_ValidBook_ReturnsAddedBook()
    {
        var newBook = FakeData.GetFakeEntityBooks()[0];
        var bookDto = FakeData.GetFakeDtoBooks()[0];

        _bookServiceMock.Setup(service => service.CreateBook(It.IsAny<BookDto>())).ReturnsAsync(newBook);

        var result = await _booksController.CreateBook(bookDto);

        var okResult = result.Result as OkObjectResult;

        Assert.NotNull(okResult);
        Assert.Equal(newBook, okResult?.Value);
    }
}
