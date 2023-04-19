using Domain;
using static Utils.EnumHelper;

namespace Application
{
    public interface IBookService
    {
        Task<IEnumerable<BookEntity>> GetBooks(BookAttribute attribute, string? value);
    }
}