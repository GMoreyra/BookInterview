using Data.Interfaces;
using Domain;
using static Utils.BookAttributeEnum;

namespace Application
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookEntity>> GetBooks(BookAttribute attribute, string? value)
        {
            switch (attribute)
            {
                case BookAttribute.Id:
                    return await _bookRepository.GetBooksById(value);

                case BookAttribute.Author:
                    return await _bookRepository.GetBooksByAuthor(value);

                case BookAttribute.Title:
                    return await _bookRepository.GetBooksByTitle(value);

                case BookAttribute.Genre:
                    return await _bookRepository.GetBooksByGenre(value);

                case BookAttribute.Description:
                    return await _bookRepository.GetBooksByDescription(value);

                case BookAttribute.Price:
                    return await _bookRepository.GetBooksByPrice(value);

                case BookAttribute.PublishDate:
                    return await _bookRepository.GetBooksByPublishDate(value);

                default:
                    return await _bookRepository.GetBooks();
            }
        }
    }
}