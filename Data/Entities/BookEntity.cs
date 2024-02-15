namespace Data.Entities
{
    public class BookEntity
    {
        public required string Id { get; set; }

        public string? Author { get; set; }

        public string? Description { get; set; }

        public string? Title { get; set; }

        public string? Genre { get; set; }

        public double? Price { get; set; }

        public DateTime? PublishDate { get; set; }
    }
}