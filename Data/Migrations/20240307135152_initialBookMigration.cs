using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initialBookMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Genre = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "Genre", "Price", "PublishDate", "Title" },
                values: new object[,]
                {
                    { "B-1", "Kutner, Joe", "Deploying with JRuby is the missing link between enjoying JRuby and using it in the real world to build high-performance, scalable applications.", "Computer", 33.0, new DateTime(2012, 8, 14, 22, 0, 0, 0, DateTimeKind.Utc), "Deploying with JRuby" },
                    { "B-10", "O'Brien, Tim", "Microsoft's .NET initiative is explored in detail in this deep programmer's reference.", "Computer", 36.950000000000003, new DateTime(2000, 12, 8, 23, 0, 0, 0, DateTimeKind.Utc), "Microsoft .NET: The Programming Bible" },
                    { "B-11", "Sydik, Jeremy J", "Accessibility has a reputation of being dull, dry, and unfriendly toward graphic design. But there is a better way: well-styled semantic markup that lets you provide the best possible results for all of your users. This book will help you provide images, video, Flash and PDF in an accessible way that looks great to your sighted users, but is still accessible to all users.", "Computer", 34.950000000000003, new DateTime(2007, 11, 30, 23, 0, 0, 0, DateTimeKind.Utc), "Design Accessible Web Sites" },
                    { "B-12", "Russell, Alex", "The last couple of years have seen big changes in server-side web programming. Now it’s the client’s turn; Dojo is the toolkit to make it happen and Mastering Dojo shows you how.", "Computer", 38.950000000000003, new DateTime(2008, 5, 31, 22, 0, 0, 0, DateTimeKind.Utc), "Mastering Dojo" },
                    { "B-13", "Copeland, David Bryant", "Speak directly to your system. With its simple commands, flags, and parameters, a well-formed command-line application is the quickest way to automate a backup, a build, or a deployment and simplify your life.", "Computer", 20.0, new DateTime(2012, 2, 29, 23, 0, 0, 0, DateTimeKind.Utc), "Build Awesome Command-Line Applications in Ruby" },
                    { "B-2", "Ralls, Kim", "A former architect battles corporate zombies, an evil sorceress, and her own childhood to become queen of the world.", "Fantasy", 5.9500000000000002, new DateTime(2000, 12, 15, 23, 0, 0, 0, DateTimeKind.Utc), "Midnight Rain" },
                    { "B-3", "Corets, Eva", "After the collapse of a nanotechnology society in England, the young survivors lay the foundation for a new society.", "Fantasy", 5.9500000000000002, new DateTime(2000, 11, 16, 23, 0, 0, 0, DateTimeKind.Utc), "Maeve Ascendant" },
                    { "B-4", "Corets, Eva", "In post-apocalypse England, the mysterious agent known only as Oberon helps to create a new life for the inhabitants of London. Sequel to Maeve Ascendant.", "Fantasy", 5.9500000000000002, new DateTime(2001, 3, 9, 23, 0, 0, 0, DateTimeKind.Utc), "Oberon's Legacy" },
                    { "B-5", "Tolkien, JRR", "If you care for journeys there and back, out of the comfortable Western world, over the edge of the Wild, and home again, and can take an interest in a humble hero blessed with a little wisdom and a little courage and considerable good luck, here is a record of such a journey and such a traveler.", "Fantasy", 11.949999999999999, new DateTime(1985, 9, 9, 22, 0, 0, 0, DateTimeKind.Utc), "The Hobbit" },
                    { "B-6", "Randall, Cynthia", "When Carla meets Paul at an ornithology conference, tempers fly as feathers get ruffled.", "Romance", 4.9500000000000002, new DateTime(2000, 9, 1, 22, 0, 0, 0, DateTimeKind.Utc), "Lover Birds" },
                    { "B-7", "Thurman, Paula", "A deep sea diver finds true love twenty thousand leagues beneath the sea.", "Romance", 4.9500000000000002, new DateTime(2000, 11, 1, 23, 0, 0, 0, DateTimeKind.Utc), "Splish Splash" },
                    { "B-8", "Knorr, Stefan", "An anthology of horror stories about roaches, centipedes, scorpions  and other insects.", "Horror", 4.9500000000000002, new DateTime(2000, 12, 5, 23, 0, 0, 0, DateTimeKind.Utc), "Creepy Crawlies" },
                    { "B-9", "Kress, Peter", "After an inadvertant trip through a Heisenberg Uncertainty Device, James Salway discovers the problems of being quantum.", "Science Fiction", 6.9500000000000002, new DateTime(2000, 11, 1, 23, 0, 0, 0, DateTimeKind.Utc), "Paradox Lost" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
