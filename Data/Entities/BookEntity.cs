namespace Data.Entities;

/// <summary>
/// Represents a book entity in the system.
/// </summary>
public class BookEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the book.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the author of the book.
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Gets or sets the description of the book.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the title of the book.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the genre of the book.
    /// </summary>
    public string? Genre { get; set; }

    /// <summary>
    /// Gets or sets the price of the book.
    /// </summary>
    public double? Price { get; set; }

    /// <summary>
    /// Gets or sets the publish date of the book.
    /// </summary>
    public DateTime? PublishDate { get; set; }
}
