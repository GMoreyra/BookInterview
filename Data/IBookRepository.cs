using Domain;

namespace Data
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookEntity>> GetBooks();
        Task<IEnumerable<BookEntity>> GetBooksById(string? id);
        Task<IEnumerable<BookEntity>> GetBooksByAuthor(string? author);
        Task<IEnumerable<BookEntity>> GetBooksByTitle(string? title);
        Task<IEnumerable<BookEntity>> GetBooksByGenre(string? genre);
        Task<IEnumerable<BookEntity>> GetBooksByDescription(string? description);
        Task<IEnumerable<BookEntity>> GetBooksByPrice(string? price);
        Task<IEnumerable<BookEntity>> GetBooksByPublishDate(string? publishDate);
        Task<BookEntity> AddBook(BookEntity book);
        Task<BookEntity> UpdateBook(BookEntity book);
    }
}