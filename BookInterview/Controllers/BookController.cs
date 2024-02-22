using Api.Attributes;
using Api.Formatters;
using Api.Validators;
using Application.DTOs;
using Application.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static Application.Enums.BookAttributeEnum;
using ResponseType = System.Net.Mime.MediaTypeNames.Application;

namespace Api.Controllers;

/// <summary>
/// Controller for managing books in the system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Consumes(ResponseType.Json)]
[Produces(ResponseType.Json)]
public class BooksController : Controller
{
    private readonly IBookService _bookService;

    /// <summary>
    /// Initializes a new instance of the BooksController class.
    /// </summary>
    /// <param name="bookService">The service to interact with books.</param>
    /// <exception cref="ArgumentNullException">Thrown when <see cref="IBookService"/> is null.</exception>
    public BooksController(IBookService bookService)
    {
        _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
    }

    /// <summary>
    /// Get all books.
    /// </summary>
    /// <returns>A list of <see cref="BookEntity"/>.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<BookEntity>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BookEntity>>> GetBooks()
    {
        var books = await _bookService.GetBooks(BookAttribute.None, null);

        return Ok(books);
    }

    /// <summary>
    /// Get books by ID.
    /// </summary>
    /// <param name="id">The ID of the book.</param>
    /// <returns>A list of <see cref="BookEntity"/>.</returns>
    [HttpGet("/id/{id?}")]
    [CheckBooksEmpty]
    [ProducesResponseType(typeof(List<BookEntity>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<BookEntity>>> GetBooksById(string? id = null)
    {
        var books = await _bookService.GetBooks(BookAttribute.Id, id);

        return Ok(books);
    }

    /// <summary>
    /// Get books by author.
    /// </summary>
    /// <param name="author">The author of the book.</param>
    /// <returns>A list of <see cref="BookEntity"/>.</returns>
    [HttpGet("/author/{author?}")]
    [CheckBooksEmpty]
    [ProducesResponseType(typeof(List<BookEntity>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<BookEntity>>> GetBooksByAuthor(string? author = null)
    {
        var books = await _bookService.GetBooks(BookAttribute.Author, author);

        return Ok(books);
    }

    /// <summary>
    /// Get books by description.
    /// </summary>
    /// <param name="description">The description of the book.</param>
    /// <returns>A list of <see cref="BookEntity"/>.</returns>
    [HttpGet("/description/{description?}")]
    [CheckBooksEmpty]
    [ProducesResponseType(typeof(List<BookEntity>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<BookEntity>>> GetBooksByDescription(string? description = null)
    {
        var books = await _bookService.GetBooks(BookAttribute.Description, description);

        return Ok(books);
    }

    /// <summary>
    /// Get books by title.
    /// </summary>
    /// <param name="title">The title of the book.</param>
    /// <returns>A list of <see cref="BookEntity"/>.</returns>
    [HttpGet("/title/{title?}")]
    [CheckBooksEmpty]
    [ProducesResponseType(typeof(List<BookEntity>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<BookEntity>>> GetBooksByTitle(string? title = null)
    {
        var books = await _bookService.GetBooks(BookAttribute.Title, title);

        return Ok(books);
    }

    /// <summary>
    /// Get books by genre.
    /// </summary>
    /// <param name="genre">The genre of the book.</param>
    /// <returns>A list of <see cref="BookEntity"/>.</returns>
    [HttpGet("/genre/{genre?}")]
    [CheckBooksEmpty]
    [ProducesResponseType(typeof(List<BookEntity>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<BookEntity>>> GetBooksByGenre(string? genre = null)
    {
        var books = await _bookService.GetBooks(BookAttribute.Genre, genre);

        return Ok(books);
    }

    /// <summary>
    /// Get books by price range.
    /// </summary>
    /// <param name="minPrice">The minimum price of the book.</param>
    /// <param name="maxPrice">The maximum price of the book.</param>
    /// <returns>A list of <see cref="BookEntity"/>.</returns>
    [HttpGet("/price")]
    [CheckBooksEmpty]
    [ProducesResponseType(typeof(List<BookEntity>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<BookEntity>>> GetBooksByPriceRange([FromQuery] double? minPrice, [FromQuery] double? maxPrice)
    {
        var validationResult = PriceValidator.ValidatePrices(minPrice, maxPrice);

        if (validationResult is not null)
        {
            return BadRequest(validationResult);
        }

        var price = PriceRangeFormatter.FormatPriceRange(minPrice, maxPrice);

        var books = await _bookService.GetBooks(BookAttribute.Price, price);

        return Ok(books);
    }

    /// <summary>
    /// Get books by publish date.
    /// </summary>
    /// <param name="year">The year of the publish date.</param>
    /// <param name="month">The month of the publish date.</param>
    /// <param name="day">The day of the publish date.</param>
    /// <returns>A list of <see cref="BookEntity"/>.</returns>
    [HttpGet("/published/{year?}/{month?}/{day?}")]
    [CheckBooksEmpty]
    [ProducesResponseType(typeof(List<BookEntity>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<BookEntity>>> GetBooksByPublishDate(int? year = null, int? month = null, int? day = null)
    {
        DateTime? parsedDate = PublishDateValidator.ParsePublishDate(year, month, day);

        if (parsedDate is null)
        {
            return BadRequest();
        }

        var books = await _bookService.GetBooks(BookAttribute.PublishDate, parsedDate.ToString());

        return Ok(books);
    }

    /// <summary>
    /// Update a book.
    /// </summary>
    /// <param name="id">The ID of the book to update.</param>
    /// <param name="book">The updated book data.</param>
    /// <returns>The updated <see cref="BookEntity"/>.</returns>
    [HttpPost("/{id}")]
    [ProducesResponseType(typeof(BookEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookEntity>> UpdateBook([Required] string id, [FromBody] BookDto book)
    {
        var updateBook = await _bookService.UpdateBook(id, book);

        return updateBook is null ? NotFound() : Ok(updateBook);
    }

    /// <summary>
    /// Add a new book.
    /// </summary>
    /// <param name="book">The book to add.</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/Books
    ///     {
    ///        "id": "1",
    ///        "title": "Book Title",
    ///        "author": "Author Name",
    ///        "description": "Book Description",
    ///        "price": 19.99,
    ///        "publishDate": "2022-01-01T00:00:00"
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    [HttpPut]
    [ProducesResponseType(typeof(BookEntity), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookEntity>> CreateBook(BookDto book)
    {
        var addedBook = await _bookService.CreateBook(book);

        return addedBook is null ? BadRequest(book) : CreatedAtAction(nameof(CreateBook), new { id = addedBook?.Id }, addedBook);
    }
}
