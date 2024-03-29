namespace Data.Repositories;

using CrossCutting.Exceptions;
using CrossCutting.Messages;
using Dapper;
using Data.Contexts;
using Data.Entities;
using Data.Extensions;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Data.Common;
using System.Globalization;

public class BookRepository : IBookRepository
{
    private readonly BookContext _bookContext;
    private readonly ILogger<BookRepository> _logger;

    private const char PriceCharSeparator = '&';
    private const string IdPrefix = "B-";

    /// <summary>
    /// Initializes a new instance of the BookRepository class.
    /// </summary>
    /// <param name="bookContext">The context to be used for accessing the database.</param>
    /// <param name="configuration">The configuration to be used for accessing app settings.</param>
    /// <param name="logger">The logger to be used for logging.</param>
    /// <exception cref="ArgumentNullException">Thrown when either <see cref="BookContext"/> or <see cref="IConfiguration"/> is null.</exception>
    public BookRepository(BookContext bookContext, ILogger<BookRepository> logger)
    {
        Argument.ThrowIfNull(() => bookContext);
        Argument.ThrowIfNull(() => logger);

        _bookContext = bookContext;
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
            query = query.Where(x => x.Id.ToUpper().Contains(id));
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
        double toleranceComparison = 0.01;

        IQueryable<BookEntity> query = _bookContext.Books;

        if (price is not null)
        {
            if (price.Contains(PriceCharSeparator, StringComparison.InvariantCultureIgnoreCase))
            {
                (var minPrice, var maxPrice) = ObtainMinAndMaxPrice(price);
                query = query.Where(x => x.Price >= minPrice && x.Price <= maxPrice);
            }
            else
            {
                var priceParsed = double.Parse(price, CultureInfo.InvariantCulture);
                query = query.Where(x => Math.Abs(x.Price - priceParsed) < toleranceComparison);
            }
        }

        return await query.OrderBy(x => x.Price).ToArrayAsync();
    }

    /// <summary>
    /// Converts a price range string into a tuple of two doubles.
    /// The price range string should be in the format "minPrice&maxPrice".
    /// </summary>
    /// <param name="price">The price range string to convert.</param>
    /// <returns>A tuple with the minimum and maximum prices as doubles.</returns>
    private static (double, double) ObtainMinAndMaxPrice(string price)
    {
        string[] prices = price.Split(PriceCharSeparator);
        double minPrice = double.Parse(prices[0], CultureInfo.InvariantCulture);
        double maxPrice = double.Parse(prices[1], CultureInfo.InvariantCulture);

        return (minPrice, maxPrice);
    }

    public async Task<IEnumerable<BookEntity>> GetBooksByPublishDate(string? publishDate)
    {
        const string YearFormat = "yyyy";
        const string YearMonthFormat = "yyyy-MM";
        const string YearMonthDayFormat = "yyyy-MM-dd";

        IQueryable<BookEntity> query = _bookContext.Books;

        if (publishDate is not null)
        {
            DateTime parsedDate;
            var formatInfo = new DateTimeFormatInfo();

            if (DateTime.TryParseExact(publishDate, YearFormat, formatInfo, DateTimeStyles.None, out parsedDate))
            {
                query = query.Where(x => x.PublishDate.Year == parsedDate.Year);
            }
            else if (DateTime.TryParseExact(publishDate, YearMonthFormat, formatInfo, DateTimeStyles.None, out parsedDate))
            {
                query = query.Where(x => x.PublishDate.Year == parsedDate.Year
                                          && x.PublishDate.Month == parsedDate.Month);
            }
            else if (DateTime.TryParseExact(publishDate, YearMonthDayFormat, formatInfo, DateTimeStyles.None, out parsedDate))
            {
                query = query.Where(x => x.PublishDate.Date == parsedDate.Date);
            }
        }

        return await query.OrderBy(x => x.PublishDate).ToArrayAsync();
    }

    public async Task<BookEntity?> AddBook(BookEntity book)
    {
        var lastId = await LastIdNumberOfEntityBook() ?? 0;

        if (lastId == 0)
        {
            return null;

        }

        book.Id = $"{IdPrefix}{lastId + 1}";

        try
        {
            var createdBook = _bookContext.Books.Add(book);

            await _bookContext.SaveChangesAsync();

            return createdBook.Entity;
        }
        catch (DbException ex)
        {
            _logger.LogError(ex, LogMessages.ErrorSavingLogMessage, book);

            return null;
        }
    }

    /// <summary>
    /// Retrieves the highest number from the book IDs in the database.
    /// The IDs are expected to follow the pattern "B-<number>".
    /// </summary>
    /// <returns>The highest number from the book IDs in the database, or null if no books exist.</returns>
    private async Task<int?> LastIdNumberOfEntityBook()
    {
        var prefix = IdPrefix;
        var prefixLength = IdPrefix.Length;

        var biggestNumberQuery = $@"
                SELECT MAX(CAST(SUBSTR(""Id"", {prefixLength + 1}, LENGTH(""Id"") - {prefixLength}) AS INT))
                FROM public.""Books""
                WHERE ""Id"" LIKE '{prefix}%'";

        try
        {
            return await _bookContext.Database.GetDbConnection()
                      .QueryFirstOrDefaultAsync<int?>(biggestNumberQuery);
        }
        catch (NpgsqlException ex)
        {
            _logger.LogError(ex, LogMessages.ErrorQueryLogMessage);

            return null;
        }
    }

    public async Task<BookEntity?> UpdateBook(BookEntity book)
    {
        try
        {
            var bookToModify = await _bookContext.Books.FindAsync(book.Id);

            if (bookToModify is null)
            {
                _logger.LogError(LogMessages.ErrorFindBookLogMessage, book.Id);

                return null;
            }

            bookToModify.UpdateProperties(book);

            await _bookContext.SaveChangesAsync();

            return bookToModify;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, LogMessages.ErrorUpdatingLogMessage, book);

            return null;
        }
    }
}
