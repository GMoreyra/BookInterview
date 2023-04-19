Working on a solution for an interview exam

# Interview-Test

## General requirements Books API:
• The API should always return a collection of books in json format.
• By default, it should return all books without any specific sorting.
• If a specific field is searched it should return the result sorted on that field.
• It should be possible to search on any field.
• When creating a new book, use the same ID standard as the rest of the books

Use cases:
Whatever field I ask for, it should return the result sorted by that field.
I should be able to ask for an author, a title, a genre, or a description. It should perform the search "case insensitive" and with partial strings. So, if I ask for "/api/books/author/kim" it should return only the book by "Ralls, Kim".
I should be able to ask for a price range or a specific price.
I should be able to ask for published_date or part of it, that means all books, books from a certain year, books from a certain year-month or books from a certain year-month-day.
I should be able to edit any field for any book using the book ID as a search parameter.
I should be able to create a new book.

Use case examples:
```
GET https://host:port/api/books returns all unsorted (B1-B13)
GET https://host:port/api/books/id returns all sorted by id (B1-B13)
GET https://host:port/api/books/id/b returns all with id containing 'b' sorted by id (B1-B13)
GET https://host:port/api/books/id/b1 returns all with id containing 'b1' sorted by id (B1, B10-13)
GET https://host:port/api/books/author returns all sorted by author (B1-B13)
GET https://host:port/api/books/author/joe returns all with author containing 'joe' sorted by author
(B1)
GET https://host:port/api/books/author/kut returns all with author containing 'kut' sorted by author
(B1)
GET https://host:port/api/books/title returns all sorted by title (B1-B13)
GET https://host:port/api/books/title/deploy returns all with title containing 'deploy' sorted by title
(B1)
GET https://host:port/api/books/title/jruby returns all with title containing 'jruby' sorted by title (B1)
GET https://host:port/api/books/genre returns all sorted by genre (B1-B13)
GET https://host:port/api/books/genre/com returns all with genre containing 'com' sorted by genre
(B1, B10-13)
GET https://host:port/api/books/genre/ter returns all with genre containing 'ter' sorted by genre
(B1, B10-13)
GET https://host:port/api/books/price returns all sorted by price (B1-B13)
GET https://host:port/api/books/price/33.0 returns all with price '33.0' (B1)
GET https://host:port/api/books/price/30.0&35.0 returns all with price between '30.0' and '35.0'
sorted by price (B1, B11)
GET https://host:port/api/books/published returns all sorted by published_date (B1-B13)
GET https://host:port/api/books/published/2012 returns all from '2012' sorted by published_date
(B13, B1)
GET https://host:port/api/books/published/2012/8 returns all from '2012-08' sorted by
published_date (B1)
GET https://host:port/api/books/published/2012/8/15 returns all from '2012-08-15' sorted by
published_date (B1)
GET https://host:port/api/books/description returns all sorted by description (B1-B13)
GET https://host:port/api/books/description/deploy returns all with description containing 'deploy'
sorted by description (B1, B13)
GET https://host:port/api/books/description/applications returns all with description containing
'applications' sorted by description (B1)

```
POST https://host:port/api/books/{id} edits an existing book
Payload
{
"author": "TestLastname, TestFirstName",
"title": "Test Book",
"genre": "Test genre",
"price": "38.95",
"publish_date": "2008-06-01",
"description": "Test description"
}

```
PUT https://host:port/api/books Creates a new entry. ID is generated in backend
Payload
{
"author": "TestLastname, TestFirstName",
"title": "Test Book",
"genre": "Test genre",
"price": "38.95",
"publish_date": "2008-06-01",
"description": "Test description"
}

