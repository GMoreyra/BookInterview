using Dapper;
using Data.Contexts;
using Data.Extensions;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using Utils;

namespace Data
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _bookContext;
        private readonly IConfiguration _configuration;

        public BookRepository(BookContext bookContext, IConfiguration configuration)
        {
            _bookContext = bookContext;
            _configuration = configuration;
        }

        public async Task<IEnumerable<BookEntity>> GetBooks()
        {
            return await _bookContext.Books.ToListAsync();
        }

        public async Task<IEnumerable<BookEntity>> GetBooksById(string? id)
        {
            IQueryable<BookEntity> query = _bookContext.Books;

            if (!string.IsNullOrWhiteSpace(id))
            {
                id = id.ToLower();
                query = query.Where(x => !string.IsNullOrWhiteSpace(x.Id) && x.Id.ToLower().Contains(id));
            }

            return await query.OrderBy(x => x.Id).ToArrayAsync();
        }

        public async Task<IEnumerable<BookEntity>> GetBooksByAuthor(string? author)
        {
            IQueryable<BookEntity> query = _bookContext.Books;

            if (!string.IsNullOrWhiteSpace(author))
            {
                author = author.ToLower();
                query = query.Where(x => !string.IsNullOrWhiteSpace(x.Author) && x.Author.ToLower().Contains(author));
            }

            return await query.OrderBy(x => x.Author).ToArrayAsync();
        }

        public async Task<IEnumerable<BookEntity>> GetBooksByTitle(string? title)
        {
            IQueryable<BookEntity> query = _bookContext.Books;

            if (!string.IsNullOrWhiteSpace(title))
            {
                title = title.ToLower();
                query = query.Where(x => !string.IsNullOrWhiteSpace(x.Title) && x.Title.ToLower().Contains(title));
            }

            return await query.OrderBy(x => x.Title).ToArrayAsync();
        }

        public async Task<IEnumerable<BookEntity>> GetBooksByGenre(string? genre)
        {
            IQueryable<BookEntity> query = _bookContext.Books;

            if (!string.IsNullOrWhiteSpace(genre))
            {
                genre = genre.ToLower();
                query = query.Where(x => !string.IsNullOrWhiteSpace(x.Genre) && x.Genre.ToLower().Contains(genre));
            }

            return await query.OrderBy(x => x.Genre).ToArrayAsync();
        }

        public async Task<IEnumerable<BookEntity>> GetBooksByDescription(string? description)
        {
            IQueryable<BookEntity> query = _bookContext.Books;

            if (!string.IsNullOrWhiteSpace(description))
            {
                description = description.ToLower();
                query = query.Where(x => !string.IsNullOrWhiteSpace(x.Description) && x.Description.ToLower().Contains(description));
            }

            return await query.OrderBy(x => x.Description).ToArrayAsync();
        }

        public async Task<IEnumerable<BookEntity>> GetBooksByPrice(string? price)
        {
            IQueryable<BookEntity> query = _bookContext.Books;

            if (price != null)
            {
                if (price.Contains('&'))
                {
                    string[] prices = price.Split('&');
                    double minPrice = Convert.ToDouble(prices[0]);
                    double maxPrice = Convert.ToDouble(prices[1]);

                    query = query.Where(x => x.Price != null && x.Price.Value >= minPrice && x.Price.Value <= maxPrice);
                }
                else
                {
                    var priceParsed = double.Parse(price);
                    query = query.Where(x => x.Price != null && x.Price.Value == priceParsed);
                }
            }

            return await query.OrderBy(x => x.Price).ToArrayAsync();
        }

        public async Task<IEnumerable<BookEntity>> GetBooksByPublishDate(string? publishDate)
        {
            IQueryable<BookEntity> query = _bookContext.Books;

            if (publishDate != null)
            {
                DateTime parsedDate;
                var formatInfo = new DateTimeFormatInfo();

                if (DateTime.TryParseExact(publishDate, "yyyy", formatInfo, DateTimeStyles.None, out parsedDate))
                {
                    query = query.Where(x => x.PublishDate != null && x.PublishDate.Value.Year == parsedDate.Year);
                }
                else if (DateTime.TryParseExact(publishDate, "yyyy-MM", formatInfo, DateTimeStyles.None, out parsedDate))
                {
                    query = query.Where(x => x.PublishDate != null && x.PublishDate.Value.Year == parsedDate.Year
                                              && x.PublishDate.Value.Month == parsedDate.Month);
                }
                else if (DateTime.TryParseExact(publishDate, "yyyy-MM-dd", formatInfo, DateTimeStyles.None, out parsedDate))
                {
                    query = query.Where(x => x.PublishDate != null && x.PublishDate.Value.Date == parsedDate.Date);
                }
            }

            return await query.OrderBy(x => x.PublishDate).ToArrayAsync();
        }

        public async Task<BookEntity> AddBook(BookEntity book)
        {
            try
            {
                var idToAdd = await GetBiggestNumber() ?? 0;
                book.Id = $"{BookUtility.ID_PREFIX}{idToAdd + 1}";

                var createdBook = _bookContext.Books.Add(book);
                await _bookContext.SaveChangesAsync();

                return createdBook.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error while saving the book.", ex);
            }
        }
        private async Task<int?> GetBiggestNumber()
        {
            var prefix = BookUtility.ID_PREFIX;
            var prefixLength = BookUtility.ID_PREFIX.Length;

            var biggestNumberQuery = $@"
                SELECT MAX(CAST(SUBSTR(Id, {prefixLength + 1}, LENGTH(Id) - {prefixLength}) AS INT))
                FROM Books
                WHERE Id LIKE '{prefix}%'";

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqliteConnection(connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<int?>(biggestNumberQuery);
            }
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
            catch (Exception ex)
            {
                throw new Exception("There was an error while updating the book.", ex);
            }
        }
    }
}