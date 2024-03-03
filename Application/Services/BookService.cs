namespace Application.Services;

using Api.Contracts.CreateBook;
using Api.Contracts.GetBooks;
using Api.Contracts.UpdateBook;
using Application.Extensions;
using Application.Interfaces;
using CrossCutting.Exceptions;
using Data.Entities;
using Data.Interfaces;
using static Application.Enums.BookAttributeEnum;

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

    public async Task<IEnumerable<GetBooksResponse>> GetBooks(BookAttribute attribute, string? value)
    {
        return await GetBooksByAttribute(attribute, value);
    }

    /// <summary>
    /// Retrieves books based on the provided attribute and value.
    /// </summary>
    /// <param name="attribute">The attribute of the book to filter by.</param>
    /// <param name="value">The value of the attribute to match.</param>
    /// <returns>A collection of books that match the provided attribute and value.</returns>
    private async Task<IEnumerable<GetBooksResponse>> GetBooksByAttribute(BookAttribute attribute, string? value)
    {
        IEnumerable<BookEntity> response = attribute switch
        {
            BookAttribute.Id => await _bookRepository.GetBooksById(value),
            BookAttribute.Author => await _bookRepository.GetBooksByAuthor(value),
            BookAttribute.Title => await _bookRepository.GetBooksByTitle(value),
            BookAttribute.Genre => await _bookRepository.GetBooksByGenre(value),
            BookAttribute.Description => await _bookRepository.GetBooksByDescription(value),
            BookAttribute.Price => await _bookRepository.GetBooksByPrice(value),
            BookAttribute.PublishDate => await _bookRepository.GetBooksByPublishDate(value),
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