
using Data.Entities;
using System.Globalization;

namespace Data.DummyGenerator;
/// <summary>
/// A class to generate dummy book entities for testing purposes.
/// </summary>
public static class DummyBookEntityGenerator
{
    /// <summary>
    /// Generates an array of dummy book entities.
    /// </summary>
    /// <returns>An array of BookEntity objects.</returns>
    public static BookEntity[] GenerateDummyBooks()
    {
        return
        [
            new() {
                Id = "B-1",
                Author = "Kutner, Joe",
                Title = "Deploying with JRuby",
                Genre = "Computer",
                Price = 33.00,
                PublishDate = DateTime.Parse("2012-08-15", CultureInfo.InvariantCulture).ToUniversalTime(),
                Description = "Deploying with JRuby is the missing link between enjoying JRuby and using it in the real world to build high-performance, scalable applications."
            },
            new() {
                Id = "B-2",
                Author = "Ralls, Kim",
                Title = "Midnight Rain",
                Genre = "Fantasy",
                Price = 5.95,
                PublishDate = DateTime.Parse("2000-12-16", CultureInfo.InvariantCulture).ToUniversalTime(),
                Description = "A former architect battles corporate zombies, an evil sorceress, and her own childhood to become queen of the world."

            },
            new() {
                Id = "B-3",
                Author = "Corets, Eva",
                Title = "Maeve Ascendant",
                Genre = "Fantasy",
                Price = 5.95,
                PublishDate = DateTime.Parse("2000-11-17", CultureInfo.InvariantCulture).ToUniversalTime(),
                Description = "After the collapse of a nanotechnology society in England, the young survivors lay the foundation for a new society."

            },
            new() {
                Id = "B-4",
                Author = "Corets, Eva",
                Title = "Oberon's Legacy",
                Genre = "Fantasy",
                Price = 5.95,
                PublishDate = DateTime.Parse("2001-03-10", CultureInfo.InvariantCulture).ToUniversalTime(),
                Description = "In post-apocalypse England, the mysterious agent known only as Oberon helps to create a new life for the inhabitants of London. Sequel to Maeve Ascendant."

            },
            new() {
                Id = "B-5",
                Author = "Tolkien, JRR",
                Title = "The Hobbit",
                Genre = "Fantasy",
                Price = 11.95,
                PublishDate = DateTime.Parse("1985-09-10", CultureInfo.InvariantCulture).ToUniversalTime(),
                Description = "If you care for journeys there and back, out of the comfortable Western world, over the edge of the Wild, and home again, and can take an interest in a humble hero blessed with a little wisdom and a little courage and considerable good luck, here is a record of such a journey and such a traveler."

            },
            new() {
                Id = "B-6",
                Author = "Randall, Cynthia",
                Title = "Lover Birds",
                Genre = "Romance",
                Price = 4.95,
                PublishDate = DateTime.Parse("2000-09-02", CultureInfo.InvariantCulture).ToUniversalTime(),
                Description = "When Carla meets Paul at an ornithology conference, tempers fly as feathers get ruffled."

            },
            new() {
                Id = "B-7",
                Author = "Thurman, Paula",
                Title = "Splish Splash",
                Genre = "Romance",
                Price = 4.95,
                PublishDate = DateTime.Parse("2000-11-02", CultureInfo.InvariantCulture).ToUniversalTime(),
                Description = "A deep sea diver finds true love twenty thousand leagues beneath the sea."

            },
            new() {
                Id = "B-8",
                Author = "Knorr, Stefan",
                Title = "Creepy Crawlies",
                Genre = "Horror",
                Price = 4.95,
                PublishDate = DateTime.Parse("2000-12-06", CultureInfo.InvariantCulture).ToUniversalTime(),
                Description = "An anthology of horror stories about roaches, centipedes, scorpions  and other insects."

            },
            new() {
                Id = "B-9",
                Author = "Kress, Peter",
                Title = "Paradox Lost",
                Genre = "Science Fiction",
                Price = 6.95,
                PublishDate = DateTime.Parse("2000-11-02", CultureInfo.InvariantCulture).ToUniversalTime(),
                Description = "After an inadvertant trip through a Heisenberg Uncertainty Device, James Salway discovers the problems of being quantum."

            },
            new() {
                Id = "B-10",
                Author = "O'Brien, Tim",
                Title = "Microsoft .NET: The Programming Bible",
                Genre = "Computer",
                Price = 36.95,
                PublishDate = DateTime.Parse("2000-12-09", CultureInfo.InvariantCulture).ToUniversalTime(),
                Description = "Microsoft's .NET initiative is explored in detail in this deep programmer's reference."

            },
            new() {
                Id = "B-11",
                Author = "Sydik, Jeremy J",
                Title = "Design Accessible Web Sites",
                Genre = "Computer",
                Price = 34.95,
                PublishDate = DateTime.Parse("2007-12-01", CultureInfo.InvariantCulture).ToUniversalTime(),
                Description = "Accessibility has a reputation of being dull, dry, and unfriendly toward graphic design. But there is a better way: well-styled semantic markup that lets you provide the best possible results for all of your users. This book will help you provide images, video, Flash and PDF in an accessible way that looks great to your sighted users, but is still accessible to all users."

            },
            new() {
                Id = "B-12",
                Author = "Russell, Alex",
                Title = "Mastering Dojo",
                Genre = "Computer",
                Price = 38.95,
                PublishDate = DateTime.Parse("2008-06-01", CultureInfo.InvariantCulture).ToUniversalTime(),
                Description = "The last couple of years have seen big changes in server-side web programming. Now it’s the client’s turn; Dojo is the toolkit to make it happen and Mastering Dojo shows you how."

            },
            new() {
                Id = "B-13",
                Author = "Copeland, David Bryant",
                Title = "Build Awesome Command-Line Applications in Ruby",
                Genre = "Computer",
                Price = 20.00,
                PublishDate = DateTime.Parse("2012-03-01", CultureInfo.InvariantCulture).ToUniversalTime(),
                Description = "Speak directly to your system. With its simple commands, flags, and parameters, a well-formed command-line application is the quickest way to automate a backup, a build, or a deployment and simplify your life."
            }
        ];
    }
}
