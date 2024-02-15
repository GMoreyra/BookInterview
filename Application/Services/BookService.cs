using Application.Interfaces;
using Data.Entities;
using Data.Interfaces;
using static Utils.BookAttributeEnum;

namespace Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BookEntity>> GetBooks(BookAttribute attribute, string? value)
    {
        return attribute switch
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
    }

    public async Task<BookEntity?> UpdateBook(BookEntity book)
    {
        return await _bookRepository.UpdateBook(book);
    }

    public async Task<BookEntity> AddBook(BookEntity book)
    {
        return await _bookRepository.AddBook(book);
    }
}