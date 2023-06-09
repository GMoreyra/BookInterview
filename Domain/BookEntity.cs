﻿namespace Domain
{
    public class BookEntity
    {
        public string Id { get; set; }

        public string? Author { get; set; }

        public string? Description { get; set; }

        public string? Title { get; set; }

        public string? Genre { get; set; }

        public double? Price { get; set; }

        public DateTime? PublishDate { get; set; }
    }
}