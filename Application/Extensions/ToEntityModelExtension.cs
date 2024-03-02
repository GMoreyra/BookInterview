namespace Application.Extensions;

using Api.Contracts.CreateBook;
using Api.Contracts.UpdateBook;
using Data.Entities;

/// <summary>
/// Provides extension methods for converting DTOs to Entity models.
/// </summary>
public static class ToEntityModelExtension
{
    /// <summary>
    /// Converts a CreateBookRequest to a BookEntity model.
    /// </summary>
    /// <param name="createBookRequest">The CreateBookRequest to convert.</param>
    /// <param name="id">The id to assign to the BookEntity. Defaults to an empty string.</param>
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
            Price = createBookRequest.Price,
            PublishDate = createBookRequest.PublishDate,
        };
    }

    /// <summary>
    /// Converts a CreateBookRequest to a BookEntity model.
    /// </summary>
    /// <param name="updateBookRequest">The CreateBookRequest to convert.</param>
    /// <param name="id">The id to assign to the BookEntity. Defaults to an empty string.</param>
    /// <returns>A <see cref="BookEntity"/>.</returns>
    public static BookEntity FromUpdateBookRequest(this UpdateBookRequest updateBookRequest, string id = " ")
    {
        return new()
        {
            Id = id,
            Author = updateBookRequest.Author ?? string.Empty,
            Description = updateBookRequest.Description ?? string.Empty,
            Title = updateBookRequest.Title ?? string.Empty,
            Genre = updateBookRequest.Genre ?? string.Empty,
            Price = updateBookRequest.Price ?? default,
            PublishDate = updateBookRequest.PublishDate ?? default,
        };
    }
}
