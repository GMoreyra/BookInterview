using Data.Entities;
using static Utils.BookAttributeEnum;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookEntity>> GetBooks(BookAttribute attribute, string? value);
        Task<BookEntity?> UpdateBook(BookEntity book);
        Task<BookEntity> AddBook(BookEntity book);
    }
}