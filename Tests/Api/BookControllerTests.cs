
using Api.Contracts.CreateBook;
using Api.Contracts.UpdateBook;
using Api.Controllers;
using Application.Enums;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Tests.Api;
public class BooksControllerTests
{
    private readonly IBookService _bookServiceMock;
    private readonly BooksController _booksController;

    public BooksControllerTests()
    {
        _bookServiceMock = Substitute.For<IBookService>();

        _booksController = new BooksController(_bookServiceMock);
    }

    [Fact]
    public void Constructor_WhenNotInitialized_ThrowsException()
    {
        // Arrange
        IBookService? nullService = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new BooksController(nullService!));
    }

    [Fact]
    public async Task GetBooks_ReturnsAllBooks()
    {
        // Arrange
        var getBooksReponse = MockData.GetBooksResponseMocks();
        _bookServiceMock.GetBooks(BookFilterBy.None, null).Returns(getBooksReponse);

        // Act
        var result = await _booksController.GetBooks();

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(getBooksReponse, okResult.Value);
    }

    [Fact]
    public async Task UpdateBook_ValidBook_ReturnsUpdatedBook()
    {
        // Arrange
        var updateBookRequest = MockData.UpdateBookRequestMocks()[0];
        var updateBookResponse = MockData.UpdateBookResponseMocks()[0];
        _bookServiceMock.UpdateBook(Arg.Any<string>(), Arg.Any<UpdateBookRequest>()).Returns(updateBookResponse);

        // Act
        var result = await _booksController.UpdateBook(string.Empty, updateBookRequest);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(updateBookResponse, okResult.Value);
    }

    [Fact]
    public async Task AddBook_ValidBook_ReturnsAddedBook()
    {
        // Arrange
        var createBookResponse = MockData.CreateBookResponseMocks()[0];
        var createBookRequest = MockData.CreateBookRequestMocks()[0];
        _bookServiceMock.CreateBook(Arg.Any<CreateBookRequest>()).Returns(createBookResponse);

        // Act
        var result = await _booksController.CreateBook(createBookRequest);

        // Assert
        var okResult = result.Result as CreatedAtActionResult;
        Assert.NotNull(okResult?.Value);
        Assert.Equal(createBookResponse, okResult?.Value);
    }
}
