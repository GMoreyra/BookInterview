namespace Data.Repositories;

using CrossCutting.Exceptions;
using Dapper;
using Data.Contexts;
using Data.Entities;
using Data.Extensions;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using System.Globalization;

public class BookRepository : IBookRepository
{
    private readonly BookContext _bookContext;
    private readonly IConfiguration _configuration;
    private readonly ILogger<BookRepository> _logger;

    private const double toleranceComparison = 0.01;
    private const string IdPrefix = "B-";
    private const string DefaultConnection = "DefaultConnection";
    private const string ErrorMessageSaving = "There was an error while saving the book: {SaveBook}.";
    private const string ErrorMessageUpdating = "There was an error while updating the book: {UpdateBook}.";

    /// <summary>
    /// Initializes a new instance of the BookRepository class.
    /// </summary>
    /// <param name="bookContext">The context to be used for accessing the database.</param>
    /// <param name="configuration">The configuration to be used for accessing app settings.</param>
    /// <param name="logger">The logger to be used for logging.</param>
    /// <exception cref="ArgumentNullException">Thrown when either <see cref="BookContext"/> or <see cref="IConfiguration"/> is null.</exception>
    public BookRepository(BookContext bookContext, IConfiguration configuration, ILogger<BookRepository> logger)
    {
        Argument.ThrowIfNull(() => bookContext);
        Argument.ThrowIfNull(() => configuration);
        Argument.ThrowIfNull(() => logger);

        _bookContext = bookContext;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<IEnumerable<BookEntity>> GetBooks()
    {
        return await _bookContext.Books.ToArrayAsync();
    }

    public async Task<IEnumerable<BookEntity>> GetBooksById(string? id)
    {
        IQueryable<BookEntity> query = _bookContext.Books;

        if (!string.IsNullOrWhiteSpace(id))
        {
            id = id.ToUpper();
            query = query.Where(x => x.Id.ToUpper().Contains(id)).OrderBy(x => x.Id);
        }

        return await query.OrderBy(x => x.Id).ToArrayAsync();

    }

    public async Task<IEnumerable<BookEntity>> GetBooksByAuthor(string? author)
    {
        IQueryable<BookEntity> query = _bookContext.Books;

        if (!string.IsNullOrWhiteSpace(author))
        {
            author = author.ToUpper();
            query = query.Where(x => x.Author.ToUpper().Contains(author));
        }

        return await query.OrderBy(x => x.Author).ToArrayAsync();
    }

    public async Task<IEnumerable<BookEntity>> GetBooksByTitle(string? title)
    {
        IQueryable<BookEntity> query = _bookContext.Books;

        if (!string.IsNullOrWhiteSpace(title))
        {
            title = title.ToUpper();
            query = query.Where(x => x.Title.ToUpper().Contains(title));
        }

        return await query.OrderBy(x => x.Title).ToArrayAsync();
    }

    public async Task<IEnumerable<BookEntity>> GetBooksByGenre(string? genre)
    {
        IQueryable<BookEntity> query = _bookContext.Books;

        if (!string.IsNullOrWhiteSpace(genre))
        {
            genre = genre.ToUpper();
            query = query.Where(x => x.Genre.ToUpper().Contains(genre));
        }

        return await query.OrderBy(x => x.Genre).ToArrayAsync();
    }

    public async Task<IEnumerable<BookEntity>> GetBooksByDescription(string? description)
    {
        IQueryable<BookEntity> query = _bookContext.Books;

        if (!string.IsNullOrWhiteSpace(description))
        {
            description = description.ToUpper();
            query = query.Where(x => x.Description.ToUpper().Contains(description));
        }

        return await query.OrderBy(x => x.Description).ToArrayAsync();
    }

    public async Task<IEnumerable<BookEntity>> GetBooksByPrice(string? price)
    {
        IQueryable<BookEntity> query = _bookContext.Books;

        if (price is not null)
        {
            if (price.Equals('&'))
            {
                (var minPrice, var maxPrice) = ParsePriceRange(price);

                query = query.Where(x => x.Price >= minPrice && x.Price <= maxPrice);
            }
            else
            {
                var priceParsed = double.Parse(price);
                query = query.Where(x => Math.Abs(x.Price - priceParsed) < toleranceComparison);
            }
        }

        return await query.OrderBy(x => x.Price).ToArrayAsync();
    }

    /// <summary>
    /// Parses the price range from a string into a tuple of two doubles.
    /// The price range is expected to be in the format "minPrice&maxPrice".
    /// </summary>
    /// <param name="price">The price range string to parse.</param>
    /// <returns>A tuple containing the minimum and maximum prices as doubles.</returns>
    private static (double, double) ParsePriceRange(string price)
    {
        string[] prices = price.Split('&');
        double minPrice = Convert.ToDouble(prices[0]);
        double maxPrice = Convert.ToDouble(prices[1]);

        return (minPrice, maxPrice);
    }

    public async Task<IEnumerable<BookEntity>> GetBooksByPublishDate(string? publishDate)
    {
        IQueryable<BookEntity> query = _bookContext.Books;

        if (publishDate is not null)
        {
            DateTime parsedDate;
            var formatInfo = new DateTimeFormatInfo();

            if (DateTime.TryParseExact(publishDate, "yyyy", formatInfo, DateTimeStyles.None, out parsedDate))
            {
                query = query.Where(x => x.PublishDate.Year == parsedDate.Year);
            }
            else if (DateTime.TryParseExact(publishDate, "yyyy-MM", formatInfo, DateTimeStyles.None, out parsedDate))
            {
                query = query.Where(x => x.PublishDate.Year == parsedDate.Year
                                          && x.PublishDate.Month == parsedDate.Month);
            }
            else if (DateTime.TryParseExact(publishDate, "yyyy-MM-dd", formatInfo, DateTimeStyles.None, out parsedDate))
            {
                query = query.Where(x => x.PublishDate.Date == parsedDate.Date);
            }
        }

        return await query.OrderBy(x => x.PublishDate).ToArrayAsync();
    }

    public async Task<BookEntity?> AddBook(BookEntity book)
    {
        try
        {
            var idToAdd = await GetBiggestNumber() ?? 0;
            book.Id = $"{IdPrefix}{idToAdd + 1}";

            var createdBook = _bookContext.Books.Add(book);
            await _bookContext.SaveChangesAsync();

            return createdBook.Entity;
        }
        catch (DbException ex)
        {
            _logger.LogError(ex, ErrorMessageSaving, book);
            return null;
        }
    }

    /// <summary>
    /// Gets the biggest number from the Ids of the books in the database.
    /// The Ids are expected to be in the format "B-<number>".
    /// </summary>
    /// <returns>The biggest number from the Ids of the books in the database, or null if there are no books.</returns>
    private async Task<int?> GetBiggestNumber()
    {
        var prefix = IdPrefix;
        var prefixLength = IdPrefix.Length;

        var biggestNumberQuery = $@"
                SELECT MAX(CAST(SUBSTR(Id, {prefixLength + 1}, LENGTH(Id) - {prefixLength}) AS INT))
                FROM Books
                WHERE Id LIKE '{prefix}%'";

        var connectionString = _configuration.GetConnectionString(DefaultConnection);

        using var connection = new Microsoft.Data.Sqlite.SqliteConnection(connectionString);

        return await connection.QueryFirstOrDefaultAsync<int?>(biggestNumberQuery);
    }

    public async Task<BookEntity?> UpdateBook(BookEntity book)
    {
        try
        {
            var bookToModify = await _bookContext.Books.FindAsync(book.Id);

            if (bookToModify is null)
            {
                return null;
            }

            bookToModify.UpdateProperties(book);

            await _bookContext.SaveChangesAsync();

            return bookToModify;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, ErrorMessageUpdating, book);
            return null;
        }
    }
}
