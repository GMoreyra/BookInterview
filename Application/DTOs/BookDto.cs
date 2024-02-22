namespace Application.DTOs;

/// <summary>
/// Data Transfer Object for Book
/// </summary>
public record BookDto
{
    /// <summary>
    /// Gets or sets the author of the book.
    /// </summary>
    public string? Author { get; init; }

    /// <summary>
    /// Gets or sets the description of the book.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the title of the book.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Gets or sets the genre of the book.
    /// </summary>
    public string? Genre { get; init; }

    /// <summary>
    /// Gets or sets the price of the book.
    /// </summary>
    public double? Price { get; init; }

    /// <summary>
    /// Gets or sets the publish date of the book.
    /// </summary>
    public DateTime? PublishDate { get; init; }
}
