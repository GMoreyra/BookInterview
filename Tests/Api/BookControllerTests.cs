namespace Tests.Api;

using global::Api.Contracts.CreateBook;
using global::Api.Contracts.UpdateBook;
using global::Api.Controllers;
using global::Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using static global::Application.Enums.BookAttributeEnum;

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
        var getBooksReponse = FakeData.GetBooksResponseMocks();
        _bookServiceMock.Setup(service => service.GetBooks(BookAttribute.None, null)).ReturnsAsync(getBooksReponse);

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
        var updateBookRequest = FakeData.UpdateBookRequestMocks()[0];
        var updateBookResponse = FakeData.UpdateBookResponseMocks()[0];
        _bookServiceMock.Setup(service => service.UpdateBook(It.IsAny<string>(), It.IsAny<UpdateBookRequest>())).ReturnsAsync(updateBookResponse);

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
        var createBookResponse = FakeData.CreateBookResponseMocks()[0];
        var createBookRequest = FakeData.CreateBookRequestMocks()[0];
        _bookServiceMock.Setup(service => service.CreateBook(It.IsAny<CreateBookRequest>())).ReturnsAsync(createBookResponse);

        // Act
        var result = await _booksController.CreateBook(createBookRequest);

        // Assert
        var okResult = result.Result as CreatedAtActionResult;
        Assert.NotNull(okResult?.Value);
        Assert.Equal(createBookResponse, okResult?.Value);
    }
}
