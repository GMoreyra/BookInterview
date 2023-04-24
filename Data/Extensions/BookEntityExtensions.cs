using Domain;

namespace Data.Extensions
{
    public static class BookEntityExtensions
    {
        public static void UpdateProperties(this BookEntity target, BookEntity source)
        {
            target.Title = source.Title ?? target.Title;
            target.Author = source.Author ?? target.Author;
            target.Genre = source.Genre ?? target.Genre;
            target.Price = source.Price ?? target.Price;
            target.PublishDate = source.PublishDate ?? target.PublishDate;
            target.Description = source.Description ?? target.Description;
        }
    }

}
