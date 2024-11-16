
using Api.Contracts.CreateBook;
using Application.Enums;
using Application.Services;
using Data.Entities;
using Data.Interfaces;
using NSubstitute;
using System.Globalization;
using Xunit;

namespace Tests.Application;
public class BookServiceTests
{
    private readonly IBookRepository _bookRepositoryMock;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _bookRepositoryMock = Substitute.For<IBookRepository>();
        _bookService = new BookService(_bookRepositoryMock);
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
        var expectedBookList = MockData.EntityBookMocks();
        var getBookResponse = MockData.GetBooksResponseMocks().Where(book =>
            (attribute == BookFilterBy.Id && book.Id == value) ||
            (attribute == BookFilterBy.Author && book.Author == value) ||
            (attribute == BookFilterBy.Title && book.Title == value) ||
            (attribute == BookFilterBy.Genre && book.Genre == value) ||
            (attribute == BookFilterBy.Description && book.Description == value) ||
            (attribute == BookFilterBy.Price && book.Price.ToString() == value) ||
            (attribute == BookFilterBy.PublishDate && book.PublishDate.ToString() == value)
        );

        _bookRepositoryMock.GetBooksById(value).Returns(expectedBookList.Where(x => x.Id == value));
        _bookRepositoryMock.GetBooksByAuthor(value).Returns(expectedBookList.Where(x => x.Author == value));
        _bookRepositoryMock.GetBooksByTitle(value).Returns(expectedBookList.Where(x => x.Title == value));
        _bookRepositoryMock.GetBooksByGenre(value).Returns(expectedBookList.Where(x => x.Genre == value));
        _bookRepositoryMock.GetBooksByDescription(value).Returns(expectedBookList.Where(x => x.Description == value));
        _bookRepositoryMock.GetBooksByPrice(value).Returns(expectedBookList.Where(x => x.Price.ToString() == value));
        _bookRepositoryMock.GetBooksByPublishDate(value).Returns(expectedBookList.Where(x => x.PublishDate.ToString() == value));

        // Act
        var result = await _bookService.GetBooks(attribute, value);

        // Assert
        Assert.Equal(getBookResponse, result);
    }

    [Fact]
    public async Task GetBooks_InvalidAttribute_ReturnsAllBooks()
    {
        // Arrange
        var expectedBooks = MockData.EntityBookMocks();
        var getBookResponse = MockData.GetBooksResponseMocks();

        _bookRepositoryMock.GetBooks().Returns(expectedBooks);

        // Act
        var result = await _bookService.GetBooks((BookFilterBy)int.MaxValue, null);

        // Assert
        Assert.Equal(getBookResponse, result);
    }

    [Fact]
    public async Task UpdateBook_ValidBook_ReturnsUpdatedBook()
    {
        // Arrange
        var entityBook = MockData.EntityBookMocks()[0];
        var updateBookRequest = MockData.UpdateBookRequestMocks()[0];
        var updateBookResponse = MockData.UpdateBookResponseMocks()[0];

        _bookRepositoryMock.UpdateBook(Arg.Any<BookEntity>()).Returns(entityBook);

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
        var createBookRequest = new CreateBookRequest("Author 11", "Test description 11", "Title 11", "Genre 11", "110", "2021-11-01");
        var createBookResponse = new CreateBookResponse("B-11", "Author 11", "Test description 11", "Title 11", "Genre 11", 110, DateTime.Parse("2021-11-01", CultureInfo.InvariantCulture));

        _bookRepositoryMock.AddBook(Arg.Any<BookEntity>()).Returns(addedBook);

        // Act
        var result = await _bookService.CreateBook(createBookRequest);

        // Assert
        Assert.Equal(createBookResponse, result);
    }
}
