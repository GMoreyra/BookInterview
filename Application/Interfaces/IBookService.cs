using Application.DTOs;
using Data.Entities;
using static Application.Enums.BookAttributeEnum;

namespace Application.Interfaces;

/// <summary>
/// Defines the contract for a service that manages books.
/// </summary>
public interface IBookService
{
    /// <summary>
    /// Fetches a list of books filtered by the specified attribute and value.
    /// </summary>
    /// <param name="attribute">The attribute to filter the books by.</param>
    /// <param name="value">The value to match the attribute.</param>
    /// <returns>An asynchronous operation that returns a collection of <see cref="BookEntity"/> objects.</returns>
    Task<IEnumerable<BookEntity>> GetBooks(BookAttribute attribute, string? value);

    /// <summary>
    /// Updates an existing book.
    /// </summary>
    /// <param name="id">The id of the book to update.</param>
    /// <param name="bookDto">The updated book data.</param>
    /// <returns>An asynchronous operation that returns the updated <see cref="BookEntity"/> object, or null if the book was not found.</returns>
    Task<BookEntity?> UpdateBook(string id, BookDto bookDto);

    /// <summary>
    /// Creates a new book.
    /// </summary>
    /// <param name="book">The book data to create a new book.</param>
    /// <returns>An asynchronous operation that returns the created <see cref="BookEntity"/> object.</returns>
    Task<BookEntity?> CreateBook(BookDto book);
}
