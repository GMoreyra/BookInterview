using Data.Entities;

namespace Data.Interfaces;

/// <summary>
/// Represents a repository for managing books.
/// </summary>
public interface IBookRepository
{
    /// <summary>
    /// Retrieves all books.
    /// </summary>
    /// <returns>A collection of books.</returns>
    Task<IEnumerable<BookEntity>> GetBooks();

    /// <summary>
    /// Retrieves books by their ID.
    /// </summary>
    /// <param name="id">The ID of the book.</param>
    /// <returns>A collection of books matching the ID.</returns>
    Task<IEnumerable<BookEntity>> GetBooksById(string? id);

    /// <summary>
    /// Retrieves books by their author.
    /// </summary>
    /// <param name="author">The author of the book.</param>
    /// <returns>A collection of books written by the author.</returns>
    Task<IEnumerable<BookEntity>> GetBooksByAuthor(string? author);

    /// <summary>
    /// Retrieves books by their title.
    /// </summary>
    /// <param name="title">The title of the book.</param>
    /// <returns>A collection of books with the specified title.</returns>
    Task<IEnumerable<BookEntity>> GetBooksByTitle(string? title);

    /// <summary>
    /// Retrieves books by their genre.
    /// </summary>
    /// <param name="genre">The genre of the book.</param>
    /// <returns>A collection of books with the specified genre.</returns>
    Task<IEnumerable<BookEntity>> GetBooksByGenre(string? genre);

    /// <summary>
    /// Retrieves books by their description.
    /// </summary>
    /// <param name="description">The description of the book.</param>
    /// <returns>A collection of books with the specified description.</returns>
    Task<IEnumerable<BookEntity>> GetBooksByDescription(string? description);

    /// <summary>
    /// Retrieves books by their price.
    /// </summary>
    /// <param name="price">The price of the book.</param>
    /// <returns>A collection of books with the specified price.</returns>
    Task<IEnumerable<BookEntity>> GetBooksByPrice(string? price);

    /// <summary>
    /// Retrieves books by their publish date.
    /// </summary>
    /// <param name="publishDate">The publish date of the book.</param>
    /// <returns>A collection of books with the specified publish date.</returns>
    Task<IEnumerable<BookEntity>> GetBooksByPublishDate(string? publishDate);

    /// <summary>
    /// Updates a book.
    /// </summary>
    /// <param name="book">The book to update.</param>
    /// <returns>The updated book.</returns>
    Task<BookEntity?> UpdateBook(BookEntity book);

    /// <summary>
    /// Adds a new book.
    /// </summary>
    /// <param name="book">The book to add.</param>
    /// <returns>The added book.</returns>
    Task<BookEntity> AddBook(BookEntity book);
}
