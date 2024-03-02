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
    public async Task GetBooks_ReturnsAllBooks()
    {
        var getBooksReponse = FakeData.GetBooksResponseMocks();
        _bookServiceMock.Setup(service => service.GetBooks(BookAttribute.None, null)).ReturnsAsync(getBooksReponse);

        var result = await _booksController.GetBooks();

        var okResult = result.Result as OkObjectResult;

        Assert.NotNull(okResult);
        Assert.Equal(getBooksReponse, okResult?.Value);
    }

    [Fact]
    public async Task UpdateBook_ValidBook_ReturnsUpdatedBook()
    {
        var updateBookRequest = FakeData.UpdateBookRequestMocks()[0];
        var updateBookResponse = FakeData.UpdateBookResponseMocks()[0];

        _bookServiceMock.Setup(service => service.UpdateBook(It.IsAny<string>(), It.IsAny<UpdateBookRequest>())).ReturnsAsync(updateBookResponse);

        var result = await _booksController.UpdateBook(string.Empty, updateBookRequest);

        var okResult = result.Result as OkObjectResult;

        Assert.NotNull(okResult);
        Assert.Equal(updateBookResponse, okResult?.Value);
    }

    [Fact]
    public async Task AddBook_ValidBook_ReturnsAddedBook()
    {
        var createBookResponse = FakeData.CreateBookResponseMocks()[0];
        var createBookRequest = FakeData.CreateBookRequestMocks()[0];

        _bookServiceMock.Setup(service => service.CreateBook(It.IsAny<CreateBookRequest>())).ReturnsAsync(createBookResponse);

        var result = await _booksController.CreateBook(createBookRequest);

        var okResult = result.Result as CreatedAtActionResult;

        Assert.NotNull(okResult?.Value);
        Assert.Equal(createBookResponse, okResult?.Value);
    }
}
