namespace Tests.Application;

using Data.Entities;
using Data.Interfaces;
using global::Api.Contracts.CreateBook;
using global::Application.Services;
using Moq;
using Xunit;
using static global::Application.Enums.BookAttributeEnum;

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
        var expectedBookList = FakeData.EntityBookMocks();
        var getBookResponse = FakeData.GetBooksResponseMocks().Where(book =>
            (attribute == BookAttribute.Id && book.Id == value) ||
            (attribute == BookAttribute.Author && book.Author == value) ||
            (attribute == BookAttribute.Title && book.Title == value) ||
            (attribute == BookAttribute.Genre && book.Genre == value) ||
            (attribute == BookAttribute.Description && book.Description == value) ||
            (attribute == BookAttribute.Price && book.Price.ToString() == value) ||
            (attribute == BookAttribute.PublishDate && book.PublishDate.ToString() == value)
);

        _bookRepositoryMock.Setup(repo => repo.GetBooksById(value)).ReturnsAsync(expectedBookList.Where(x => x.Id == value));
        _bookRepositoryMock.Setup(repo => repo.GetBooksByAuthor(value)).ReturnsAsync(expectedBookList.Where(x => x.Author == value));
        _bookRepositoryMock.Setup(repo => repo.GetBooksByTitle(value)).ReturnsAsync(expectedBookList.Where(x => x.Title == value));
        _bookRepositoryMock.Setup(repo => repo.GetBooksByGenre(value)).ReturnsAsync(expectedBookList.Where(x => x.Genre == value));
        _bookRepositoryMock.Setup(repo => repo.GetBooksByDescription(value)).ReturnsAsync(expectedBookList.Where(x => x.Description == value));
        _bookRepositoryMock.Setup(repo => repo.GetBooksByPrice(value)).ReturnsAsync(expectedBookList.Where(x => x.Price.ToString() == value));
        _bookRepositoryMock.Setup(repo => repo.GetBooksByPublishDate(value)).ReturnsAsync(expectedBookList.Where(x => x.PublishDate.ToString() == value));

        var result = await _bookService.GetBooks(attribute, value);

        Assert.Equal(getBookResponse, result);
    }

    [Fact]
    public async Task GetBooks_InvalidAttribute_ReturnsAllBooks()
    {
        var expectedBooks = FakeData.EntityBookMocks();
        var getBookResponse = FakeData.GetBooksResponseMocks();

        _bookRepositoryMock.Setup(repo => repo.GetBooks()).ReturnsAsync(expectedBooks);

        var result = await _bookService.GetBooks((BookAttribute)int.MaxValue, null);

        Assert.Equal(getBookResponse, result);
    }

    [Fact]
    public async Task UpdateBook_ValidBook_ReturnsUpdatedBook()
    {
        var entityBook = FakeData.EntityBookMocks()[0];
        var updateBookRequest = FakeData.UpdateBookRequestMocks()[0];
        var updateBookResponse = FakeData.UpdateBookResponseMocks()[0];

        _bookRepositoryMock.Setup(repo => repo.UpdateBook(It.IsAny<BookEntity>())).ReturnsAsync(entityBook);

        var result = await _bookService.UpdateBook(string.Empty, updateBookRequest);

        Assert.Equal(updateBookResponse, result);
    }

    [Fact]
    public async Task CreateBook_ValidBook_ReturnsAddedBook()
    {
        var addedBook = new BookEntity { Id = "B-11", Author = "Author 11", Title = "Title 11", Genre = "Genre 11", Price = 110, Description = "Test description 11", PublishDate = DateTime.Parse("2021-11-01") };
        var createBookRequest = new CreateBookRequest("Author 11", "Test description 11", "Title 11", "Genre 11", 110, DateTime.Parse("2021-11-01"));
        var createBookResponse = new CreateBookResponse("B-11", "Author 11", "Test description 11", "Title 11", "Genre 11", 110, DateTime.Parse("2021-11-01"));

        _bookRepositoryMock.Setup(repo => repo.AddBook(It.IsAny<BookEntity>())).ReturnsAsync(addedBook);

        var result = await _bookService.CreateBook(createBookRequest);

        Assert.Equal(createBookResponse, result);
    }
}
