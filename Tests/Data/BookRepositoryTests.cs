using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Tests.Data;

public class BookRepositoryTests
{
    private readonly IConfiguration _configuration;
    private readonly BookContext _bookContext;
    private readonly BookRepository _bookRepository;

    public BookRepositoryTests()
    {
        _bookContext = CreateMockBookContext();
        _configuration = CreateMockConfiguration();
        _bookRepository = new BookRepository(_bookContext, _configuration);
    }

    [Fact]
    public async Task GetBooks_ShouldReturnAllBooks()
    {
        var result = await _bookRepository.GetBooks();

        Assert.Equal(10, result?.Count());
    }

    [Fact]
    public async Task GetBooksById_ShouldReturnBooksById()
    {
        var result = await _bookRepository.GetBooksById("B-1");

        Assert.Equal(1, result?.Count());
        Assert.Equal("B-1", result?.First().Id);
    }

    [Fact]
    public async Task GetBooksById_ShouldReturnAllBooks_OrderedById()
    {
        var result = await _bookRepository.GetBooksById(null);

        Assert.Equal(10, result?.Count());
        Assert.Equal("B-1", result?.First().Id);
    }

    [Fact]
    public async Task GetBooksByAuthor_ShouldReturnBooksByAuthor()
    {
        var result = await _bookRepository.GetBooksByAuthor("Author 1");

        Assert.Equal(1, result?.Count());
        Assert.Equal("Author 1", result?.First().Author);
    }

    [Fact]
    public async Task GetBooksByAuthor_ShouldReturnAllBooks_OrderedByAuthor()
    {
        var result = await _bookRepository.GetBooksByAuthor(null);

        Assert.Equal(10, result?.Count());
        Assert.Equal("Author 1", result?.First().Author);
    }

    [Fact]
    public async Task GetBooksByTitle_ShouldReturnBooksByTitle()
    {
        var result = await _bookRepository.GetBooksByTitle("Title 1");

        Assert.Equal(1, result?.Count());
        Assert.Equal("Author 1", result?.First().Title);
    }

    [Fact]
    public async Task GetBooksByTitle_ShouldReturnAllBooks_OrderedByTitle()
    {
        var result = await _bookRepository.GetBooksByAuthor(null);

        Assert.Equal(10, result?.Count());
        Assert.Equal("Author 1", result?.First().Title);
    }

    [Fact]
    public async Task GetBooksByGenre_ShouldReturnBooksByGenre()
    {
        var result = await _bookRepository.GetBooksByGenre("Genre 1");

        Assert.Equal(1, result?.Count());
        Assert.Equal("Genre 1", result?.First().Genre);
    }

    [Fact]
    public async Task GetBooksByDescription_ShouldReturnBooksByDescription()
    {
        var result = await _bookRepository.GetBooksByDescription("Test description 1");

        Assert.Equal(1, result?.Count());
        Assert.Equal("Test description 1", result?.First().Description);
    }

    [Fact]
    public async Task GetBooksByPrice_ShouldReturnBooksByPrice()
    {
        var result = await _bookRepository.GetBooksByPrice("10");

        Assert.Equal(1, result?.Count());
        Assert.Equal(10, result?.First().Price);

        result = await _bookRepository.GetBooksByPrice("10&30");

        Assert.Equal(3, result?.Count());
        Assert.True(result?.All(b => b.Price >= 10 && b.Price <= 30));
    }

    [Fact]
    public async Task GetBooksByPublishDate_ShouldReturnBooksByPublishDate()
    {
        var result = await _bookRepository.GetBooksByPublishDate("2021");

        Assert.Equal(10, result?.Count());

        result = await _bookRepository.GetBooksByPublishDate("2021-01");

        Assert.Equal(1, result?.Count());
        Assert.Equal("2021-01-01", result?.First().PublishDate?.ToString("yyyy-MM-dd"));

        result = await _bookRepository.GetBooksByPublishDate("2021-01-01");

        Assert.Equal(1, result?.Count());
        Assert.Equal("2021-01-01", result?.First().PublishDate?.ToString("yyyy-MM-dd"));
    }

    private static BookContext CreateMockBookContext()
    {
        var fakeBooks = FakeData.GetFakeEntityBooks().AsQueryable();
        var asyncFakeBooks = fakeBooks.AsAsyncEnumerable();

        var mockSet = new Mock<DbSet<BookEntity>>();

        mockSet.As<IAsyncEnumerable<BookEntity>>().Setup(m => m.GetAsyncEnumerator(default)).Returns(asyncFakeBooks.GetAsyncEnumerator());
        mockSet.As<IQueryable<BookEntity>>().Setup(m => m.Provider).Returns(fakeBooks.Provider);
        mockSet.As<IQueryable<BookEntity>>().Setup(m => m.Expression).Returns(fakeBooks.Expression);
        mockSet.As<IQueryable<BookEntity>>().Setup(m => m.ElementType).Returns(fakeBooks.ElementType);
        mockSet.As<IQueryable<BookEntity>>().Setup(m => m.GetEnumerator()).Returns(fakeBooks.GetEnumerator());

        var mockContext = new Mock<BookContext>(new DbContextOptions<BookContext>());
        mockContext.Setup(c => c.Books).Returns(mockSet.Object);

        return mockContext.Object;
    }

    private static IConfiguration CreateMockConfiguration()
    {
        var configurationSectionMock = new Mock<IConfigurationSection>();
        configurationSectionMock.Setup(x => x.Value).Returns("Data Source=:memory:");

        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(x => x.GetSection("ConnectionStrings:DefaultConnection")).Returns(configurationSectionMock.Object);

        return configurationMock.Object;
    }
}
