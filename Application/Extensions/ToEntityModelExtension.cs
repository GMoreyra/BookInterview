using Application.DTOs;
using Data.Entities;

namespace Application.Extensions;

/// <summary>
/// Provides extension methods for converting DTOs to Entity models.
/// </summary>
public static class ToEntityModelExtension
{
    /// <summary>
    /// Converts a BookDto to a BookEntity model.
    /// </summary>
    /// <param name="bookDto">The BookDto to convert.</param>
    /// <param name="id">The id to assign to the BookEntity. Defaults to an empty string.</param>
    /// <returns>A <see cref="BookEntity"/>.</returns>
    public static BookEntity FromBookDto(this BookDto bookDto, string id = " ")
    {
        return new()
        {
            Id = id,
            Author = bookDto.Author,
            Description = bookDto.Description,
            Title = bookDto.Title,
            Genre = bookDto.Genre,
            Price = bookDto.Price,
            PublishDate = bookDto.PublishDate
        };
    }
}
