using Data.Entities;
using static Utils.BookAttributeEnum;

namespace Application.Interfaces;

/// <summary>
/// Represents the interface for a book service.
/// </summary>
public interface IBookService
{
    /// <summary>
    /// Retrieves a list of books based on the specified attribute and value.
    /// </summary>
    /// <param name="attribute">The attribute to filter the books by.</param>
    /// <param name="value">The value to match the attribute.</param>
    /// <returns>An asynchronous operation that returns a collection of BookEntity objects.</returns>
    Task<IEnumerable<BookEntity>> GetBooks(BookAttribute attribute, string? value);

    /// <summary>
    /// Updates a book.
    /// </summary>
    /// <param name="book">The book to update.</param>
    /// <returns>An asynchronous operation that returns the updated BookEntity object, or null if the book was not found.</returns>
    Task<BookEntity?> UpdateBook(BookEntity book);

    /// <summary>
    /// Adds a new book.
    /// </summary>
    /// <param name="book">The book to add.</param>
    /// <returns>An asynchronous operation that returns the added BookEntity object.</returns>
    Task<BookEntity> AddBook(BookEntity book);
}
