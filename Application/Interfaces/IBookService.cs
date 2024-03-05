namespace Application.Interfaces;

using Api.Contracts.CreateBook;
using Api.Contracts.GetBooks;
using Api.Contracts.UpdateBook;
using Application.Enums;

/// <summary>
/// Defines the contract for a service that manages books in the application.
/// </summary>
public interface IBookService
{
    /// <summary>
    /// Fetches a list of books from the database, filtered by the specified attribute and value.
    /// </summary>
    /// <param name="attribute">The attribute to filter the books by.</param>
    /// <param name="value">The value to match the attribute.</param>
    /// <returns>An asynchronous operation that returns a collection of <see cref="GetBooksResponse"/> objects.</returns>
    Task<IEnumerable<GetBooksResponse>> GetBooks(BookFilterBy attribute, string? value);

    /// <summary>
    /// Updates the details of an existing book in the database.
    /// </summary>
    /// <param name="id">The id of the book to update.</param>
    /// <param name="bookDto">The updated book data.</param>
    /// <returns>An asynchronous operation that returns the updated <see cref="UpdateBookResponse"/> object, or null if the book was not found.</returns>
    Task<UpdateBookResponse?> UpdateBook(string id, UpdateBookRequest updateBookRequest);

    /// <summary>
    /// Creates a new book in the database using the provided book data.
    /// </summary>
    /// <param name="book">The book data to create a new book.</param>
    /// <returns>An asynchronous operation that returns the created <see cref="CreateBookResponse"/> object.</returns>
    Task<CreateBookResponse?> CreateBook(CreateBookRequest createBookRequest);
}
