using Data.Entities;

namespace Data.Extensions;

/// <summary>
/// Provides extension methods for the BookEntity class.
/// </summary>
public static class BookEntityExtensions
{
    /// <summary>
    /// Updates the properties of the target BookEntity with the properties of the source BookEntity.
    /// If a property in the source BookEntity is null, the corresponding property in the target BookEntity is not updated.
    /// </summary>
    /// <param name="target">The BookEntity to update.</param>
    /// <param name="source">The BookEntity with the new values.</param>
    public static void UpdateProperties(this BookEntity target, BookEntity source)
    {
        target.Title = source.Title;
        target.Author = source.Author;
        target.Genre = source.Genre;
        target.Price = source.Price;
        target.PublishDate = source.PublishDate;
        target.Description = source.Description;
    }
}
