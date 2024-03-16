# Interview Book Test

## General requirements Books API:
* The API should return a collection of books in JSON format.
* By default, return all books without specific sorting.
* Allow searching and sorting on any field.
* When creating a new book, use the same ID standard as existing books.

## Use cases:
* Enable field-based search and sorting (e.g., author, title, genre, description).
* Support case-insensitive and partial string search.
* Allow searching by price range or specific price.
* Enable search by published_date or parts of it (e.g., year, year-month, year-month-day).
* Permit editing any field for a book using its ID.
* Allow creating a new book.

## Use case examples:
```
GET https://host:port/api/books returns all unsorted (B1-B13)
GET https://host:port/api/books/id returns all sorted by id (B1-B13)
GET https://host:port/api/books/id/b returns all with id containing 'b' sorted by id (B1-B13)
GET https://host:port/api/books/id/b1 returns all with id containing 'b1' sorted by id (B1, B10-13)
GET https://host:port/api/books/author returns all sorted by author (B1-B13)
GET https://host:port/api/books/author/joe returns all with author containing 'joe' sorted by author (B1)
GET https://host:port/api/books/author/kut returns all with author containing 'kut' sorted by author (B1)
GET https://host:port/api/books/title returns all sorted by title (B1-B13)
GET https://host:port/api/books/title/deploy returns all with title containing 'deploy' sorted by title (B1)
GET https://host:port/api/books/title/jruby returns all with title containing 'jruby' sorted by title (B1)
GET https://host:port/api/books/genre returns all sorted by genre (B1-B13)
GET https://host:port/api/books/genre/com returns all with genre containing 'com' sorted by genre (B1, B10-13)
GET https://host:port/api/books/genre/ter returns all with genre containing 'ter' sorted by genre (B1, B10-13)
GET https://host:port/api/books/price returns all sorted by price (B1-B13)
GET https://host:port/api/books/price/33.0 returns all with price '33.0' (B1)
GET https://host:port/api/books/price/30.0&35.0 returns all with price between '30.0' and '35.0' sorted by price (B1, B11)
GET https://host:port/api/books/published returns all sorted by published_date (B1-B13)
GET https://host:port/api/books/published/2012 returns all from '2012' sorted by published_date (B13, B1)
GET https://host:port/api/books/published/2012/8 returns all from '2012-08' sorted by published_date (B1)
GET https://host:port/api/books/published/2012/8/15 returns all from '2012-08-15' sorted by published_date (B1)
GET https://host:port/api/books/description returns all sorted by description (B1-B13)
GET https://host:port/api/books/description/deploy returns all with description containing 'deploy' sorted by description (B1, B13)
GET https://host:port/api/books/description/applications returns all with description containing 'applications' sorted by description (B1)
```
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
```

## Implementation:

This API is implemented using .NET 8 for the backend, PostgreSQL for the database, and Dapper and Entity Framework for data access.

## Additional Features:

### SonarAnalyzer:

This project also uses SonarAnalyzer, a set of Roslyn analyzers that report on various types of code smells, bugs, and security vulnerabilities. This helps to maintain high code quality and identify potential issues early in the development process.

### Authentication:

The API includes a dummy authentication system using JWT (JSON Web Tokens). This is a simple, hardcoded system for demonstration purposes and should be replaced with a full authentication system for any real-world application.

### Docker and Docker Compose:

This project also includes Docker and Docker Compose configurations. This allows for easy setup and deployment of the API and the PostgreSQL database. To start the services, simply run `docker-compose up` in the root directory of the project.

