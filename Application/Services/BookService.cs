namespace Application.Services;

using Api.Contracts.CreateBook;
using Api.Contracts.GetBooks;
using Api.Contracts.UpdateBook;
using Application.Enums;
using Application.Interfaces;
using Application.MapExtensions;
using CrossCutting.Exceptions;
using Data.Entities;
using Data.Interfaces;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    /// <summary>
    /// Initializes a new instance of the BookService class.
    /// </summary>
    /// <param name="bookRepository">The book repository.</param>
    /// <exception cref="ArgumentNullException">Thrown when <see cref="IBookRepository"/> is null.</exception>
    public BookService(IBookRepository bookRepository)
    {
        Argument.ThrowIfNull(() => bookRepository);

        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<GetBooksResponse>> GetBooks(BookFilterBy attribute, string? value)
    {
        return await GetBooksByAttribute(attribute, value);
    }

    /// <summary>
    /// Retrieves books based on the provided attribute and value.
    /// </summary>
    /// <param name="attribute">The attribute of the book to filter by.</param>
    /// <param name="value">The value of the attribute to match.</param>
    /// <returns>A collection of books that match the provided attribute and value.</returns>
    private async Task<IEnumerable<GetBooksResponse>> GetBooksByAttribute(BookFilterBy attribute, string? value)
    {
        IEnumerable<BookEntity> response = attribute switch
        {
            BookFilterBy.Id => await _bookRepository.GetBooksById(value),
            BookFilterBy.Author => await _bookRepository.GetBooksByAuthor(value),
            BookFilterBy.Title => await _bookRepository.GetBooksByTitle(value),
            BookFilterBy.Genre => await _bookRepository.GetBooksByGenre(value),
            BookFilterBy.Description => await _bookRepository.GetBooksByDescription(value),
            BookFilterBy.Price => await _bookRepository.GetBooksByPrice(value),
            BookFilterBy.PublishDate => await _bookRepository.GetBooksByPublishDate(value),
            _ => await _bookRepository.GetBooks(),
        };

        IEnumerable<GetBooksResponse> books = response.Select(x => x.ToGetBooksResponse());

        return books;
    }

    public async Task<UpdateBookResponse?> UpdateBook(string id, UpdateBookRequest updateBookRequest)
    {
        var bookEntity = updateBookRequest.FromUpdateBookRequest(id);

        var updatedBook = await _bookRepository.UpdateBook(bookEntity);

        return updatedBook?.ToUpdateBookResponse();
    }

    public async Task<CreateBookResponse?> CreateBook(CreateBookRequest createBookRequest)
    {
        var bookEntity = createBookRequest.FromCreateBookRequest();

        var bookAdded = await _bookRepository.AddBook(bookEntity);

        return bookAdded?.ToCreateBookResponse();
    }
}
