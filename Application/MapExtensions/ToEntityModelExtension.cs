
using Api.Contracts.CreateBook;
using Api.Contracts.UpdateBook;
using Data.Entities;
using System.Globalization;

namespace Application.MapExtensions;
/// <summary>
/// Provides extension methods for converting DTOs to Entity models.
/// </summary>
public static class ToEntityModelExtension
{
    /// <summary>
    /// Converts a CreateBookRequest to a BookEntity model.
    /// </summary>
    /// <param name="createBookRequest">The CreateBookRequest to convert.</param>
    /// <returns>A <see cref="BookEntity"/>.</returns>
    public static BookEntity FromCreateBookRequest(this CreateBookRequest createBookRequest)
    {
        return new()
        {
            Id = string.Empty,
            Author = createBookRequest.Author,
            Description = createBookRequest.Description,
            Title = createBookRequest.Title,
            Genre = createBookRequest.Genre,
            Price = double.Parse(createBookRequest.Price, CultureInfo.InvariantCulture),
            PublishDate = DateTime.Parse(createBookRequest.PublishDate, CultureInfo.InvariantCulture),
        };
    }

    /// <summary>
    /// Converts an UpdateBookRequest to a BookEntity model.
    /// </summary>
    /// <param name="updateBookRequest">The UpdateBookRequest to convert.</param>
    /// <param name="id">The id to assign to the BookEntity.</param>
    /// <returns>A <see cref="BookEntity"/>.</returns>
    public static BookEntity FromUpdateBookRequest(this UpdateBookRequest updateBookRequest, string id)
    {
        return new()
        {
            Id = id,
            Author = updateBookRequest.Author ?? string.Empty,
            Description = updateBookRequest.Description ?? string.Empty,
            Title = updateBookRequest.Title ?? string.Empty,
            Genre = updateBookRequest.Genre ?? string.Empty,
            Price = string.IsNullOrWhiteSpace(updateBookRequest.Price) ? default : double.Parse(updateBookRequest.Price, CultureInfo.InvariantCulture),
            PublishDate = string.IsNullOrWhiteSpace(updateBookRequest.PublishDate) ? default : DateTime.Parse(updateBookRequest.PublishDate, CultureInfo.InvariantCulture),
        };
    }
}
