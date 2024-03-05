namespace Application.Enums;

/// <summary>
/// Static class containing the BookFilterBy enum.
/// </summary>
public static class BookFilterEnum
{
    /// <summary>
    /// Enum for specifying the attribute to filter books by.
    /// </summary>
    public enum BookFilterBy
    {
        None = 0,
        Id,
        Author,
        Title,
        Genre,
        Price,
        PublishDate,
        Description
    }
}
