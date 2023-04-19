using Data.Interfaces;
using Domain;
using Utils;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _bookContext;

        public BookRepository(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<IEnumerable<BookEntity>> GetBooks()
        {
            return await _bookContext.Books.ToListAsync();
        }

        public async Task<IEnumerable<BookEntity>> GetBooksById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return await _bookContext.Books.OrderBy(x => x.Id).ToArrayAsync();
            }

            var idParsed = int.Parse(id);
            return await _bookContext.Books.Where(x => x.Id == idParsed).OrderBy(x => x.Id ).ToArrayAsync();
        }

        public async Task<IEnumerable<BookEntity>> GetBooksByAuthor(string? author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                return await _bookContext.Books.OrderBy(x => x.Author).ToArrayAsync();
            }
            
            author = author.ToLower();
            return await _bookContext.Books
                .Where(x=> !string.IsNullOrWhiteSpace(x.Author) && x.Author.ToLower().Contains(author))
                .OrderBy(x => x.Author).ToArrayAsync();
        }

        public async Task<IEnumerable<BookEntity>> GetBooksByTitle(string? title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return await _bookContext.Books.OrderBy(x => x.Title).ToArrayAsync();
            }

            title = title.ToLower();
            return await _bookContext.Books
                .Where(x => !string.IsNullOrWhiteSpace(x.Title) && x.Title.ToLower().Contains(title))
                .OrderBy(x => x.Title).ToArrayAsync();
        }

        public async Task<IEnumerable<BookEntity>> GetBooksByGenre(string? genre)
        {
            if (string.IsNullOrWhiteSpace(genre))
            {
                return await _bookContext.Books.OrderBy(x => x.Genre).ToArrayAsync();
            }

            genre = genre.ToLower();
            return await _bookContext.Books
                .Where(x => !string.IsNullOrWhiteSpace(x.Genre) && x.Genre.ToLower().Contains(genre))
                .OrderBy(x => x.Genre).ToArrayAsync();
        }

        public async Task<IEnumerable<BookEntity>> GetBooksByDescription(string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return await _bookContext.Books.OrderBy(x => x.Description).ToArrayAsync();
            }

            description = description.ToLower();
            return await _bookContext.Books
                .Where(x => !string.IsNullOrWhiteSpace(x.Description) && x.Description.ToLower().Contains(description))
                .OrderBy(x => x.Description).ToArrayAsync();
        }

        public async Task<IEnumerable<BookEntity>> GetBooksByPrice(string? price)
        {
            if (price is null)
            {
                return await _bookContext.Books.OrderBy(x => x.Price).ToArrayAsync();
            }

            if (price.Contains('&'))
            {
                string[] prices = price.Split('&');
                double minPrice = Convert.ToDouble(prices[0]);
                double maxPrice = Convert.ToDouble(prices[1]);

                return await _bookContext.Books
                .Where(x => x.Price != null && x.Price.Value >= minPrice && x.Price.Value <= maxPrice)
                .OrderBy(x => x.Price).ToArrayAsync();
            }

            return await _bookContext.Books
                .Where(x => x.Price != null && x.Price.Value == ValidatorHelper.ParseDouble(price))
                .OrderBy(x => x.Price).ToArrayAsync();
        }

        public async Task<IEnumerable<BookEntity>> GetBooksByPublishDate(string? publishDate)
        {
            if (publishDate is null)
            {
                return await _bookContext.Books.OrderBy(x => x.PublishDate).ToArrayAsync();
            }

            return await _bookContext.Books
                .Where(x => x.PublishDate != null && x.PublishDate.Value == ValidatorHelper.ParseDateTime(publishDate))
                .OrderBy(x => x.Price).ToArrayAsync();
        }

        public async Task<BookEntity> AddBook(BookEntity book)
        {
            var createdBook = _bookContext.Books.Add(book);
            await _bookContext.SaveChangesAsync();

            return createdBook.Entity;
        }

        public async Task<BookEntity> UpdateBook(BookEntity book)
        {
            var existingBook = await _bookContext.Books.FindAsync(book.Id);
            if (existingBook != null)
            {
                existingBook.Title = ValidatorHelper.ValidateString(book.Title) ? book.Title : existingBook.Title;
                existingBook.Author = ValidatorHelper.ValidateString(book.Author) ? book.Author : existingBook.Author;
                existingBook.Genre = ValidatorHelper.ValidateString(book.Genre) ? book.Genre : existingBook.Genre ;
                existingBook.Price = book.Price ?? existingBook.Price;
                existingBook.PublishDate = book.PublishDate ?? existingBook.PublishDate;
                existingBook.Description = ValidatorHelper.ValidateString(book.Description) ? book.Description : existingBook.Description;

                await _bookContext.SaveChangesAsync();

                return existingBook;
            }

            return book;
        }
    }
}