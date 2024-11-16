
using Api.Contracts.CreateBook;
using Api.Contracts.GetBooks;
using Api.Contracts.UpdateBook;
using Data.Entities;

namespace Application.MapExtensions;
public static class ToContractExtension
{
    public static CreateBookResponse ToCreateBookResponse(this BookEntity bookEntity)
    {
        return new(bookEntity.Id,
                   bookEntity.Author,
                   bookEntity.Description,
                   bookEntity.Title,
                   bookEntity.Genre,
                   bookEntity.Price,
                   bookEntity.PublishDate);
    }

    public static UpdateBookResponse ToUpdateBookResponse(this BookEntity bookEntity)
    {
        return new(bookEntity.Id,
                   bookEntity.Author,
                   bookEntity.Description,
                   bookEntity.Title,
                   bookEntity.Genre,
                   bookEntity.Price,
                   bookEntity.PublishDate);
    }

    public static GetBooksResponse ToGetBooksResponse(this BookEntity bookEntity)
    {
        return new(bookEntity.Id,
                   bookEntity.Author,
                   bookEntity.Description,
                   bookEntity.Title,
                   bookEntity.Genre,
                   bookEntity.Price,
                   bookEntity.PublishDate);
    }
}
