using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Tests;

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

        result.Should().HaveCount(10);
    }

    [Fact]
    public async Task GetBooksById_ShouldReturnBooksById()
    {
        var result = await _bookRepository.GetBooksById("B-1");
        result.Should().HaveCount(1);
        result.First().Id.Should().Be("B-1");
    }

    [Fact]
    public async Task GetBooksById_ShouldReturnAllBooks_OrderedById()
    {
        var result = await _bookRepository.GetBooksById(null);
        result.Should().HaveCount(10);
        result.First().Id.Should().Be("B-1");
    }

    [Fact]
    public async Task GetBooksByAuthor_ShouldReturnBooksByAuthor()
    {
        var result = await _bookRepository.GetBooksByAuthor("Author 1");
        result.Should().HaveCount(1);
        result.First().Author.Should().Be("Author 1");
    }

    [Fact]
    public async Task GetBooksByAuthor_ShouldReturnAllBooks_OrderedByAuthor()
    {
        var result = await _bookRepository.GetBooksByAuthor(null);
        result.Should().HaveCount(10);
        result.First().Author.Should().Be("Author 1");
    }

    [Fact]
    public async Task GetBooksByTitle_ShouldReturnBooksByTitle()
    {
        var result = await _bookRepository.GetBooksByTitle("Title 1");
        result.Should().HaveCount(1);
        result.First().Title.Should().Be("Title 1");
    }

    [Fact]
    public async Task GetBooksByGenre_ShouldReturnBooksByGenre()
    {
        var result = await _bookRepository.GetBooksByGenre("Genre 1");
        result.Should().HaveCount(1);
        result.First().Genre.Should().Be("Genre 1");
    }

    [Fact]
    public async Task GetBooksByDescription_ShouldReturnBooksByDescription()
    {
        var result = await _bookRepository.GetBooksByDescription("Test description 1");
        result.Should().HaveCount(1);
        result.First().Description.Should().Be("Test description 1");
    }

    [Fact]
    public async Task GetBooksByPrice_ShouldReturnBooksByPrice()
    {
        var result = await _bookRepository.GetBooksByPrice("10");
        result.Should().HaveCount(1);
        result.First().Price.Should().Be(10);

        result = await _bookRepository.GetBooksByPrice("10&30");
        result.Should().HaveCount(3);
        result.All(b => b.Price >= 10 && b.Price <= 30).Should().BeTrue();
    }

    [Fact]
    public async Task GetBooksByPublishDate_ShouldReturnBooksByPublishDate()
    {
        var result = await _bookRepository.GetBooksByPublishDate("2021");
        result.Should().HaveCount(10);

        result = await _bookRepository.GetBooksByPublishDate("2021-01");
        result.Should().HaveCount(1);
        result.First().PublishDate.Should().Be(DateTime.Parse("2021-01-01"));

        result = await _bookRepository.GetBooksByPublishDate("2021-01-01");
        result.Should().HaveCount(1);
        result.First().PublishDate.Should().Be(DateTime.Parse("2021-01-01"));
    }

    private static BookContext CreateMockBookContext()
    {
        var fakeBooks = FakeData.GetFakeBooks().AsQueryable();
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
