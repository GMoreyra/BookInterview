using Api.Attributes;
using Application.DTOs;
using Application.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Utils;
using static Utils.BookAttributeEnum;
using ResponseType = System.Net.Mime.MediaTypeNames.Application;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes(ResponseType.Json)]
[Produces(ResponseType.Json)]
public class BooksController : Controller
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    /// <summary>
    /// Get all books.
    /// </summary>
    /// <returns>A list of books.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookEntity>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooks()
    {
        var books = await _bookService.GetBooks(BookAttribute.None, null);

        return Ok(books);
    }

    /// <summary>
    /// Get books by ID.
    /// </summary>
    /// <param name="id">The ID of the book.</param>
    /// <returns>A list of books.</returns>
    [HttpGet("/id/{id?}")]
    [CheckBooksEmpty]
    [ProducesResponseType(typeof(IEnumerable<BookEntity>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksById(string? id = null)
    {
        var books = await _bookService.GetBooks(BookAttribute.Id, id);

        return Ok(books);
    }

    /// <summary>
    /// Get books by author.
    /// </summary>
    /// <param name="author">The author of the book.</param>
    /// <returns>A list of books.</returns>
    [HttpGet("/author/{author?}")]
    [CheckBooksEmpty]
    [ProducesResponseType(typeof(IEnumerable<BookEntity>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByAuthor(string? author = null)
    {
        var books = await _bookService.GetBooks(BookAttribute.Author, author);

        return Ok(books);
    }

    /// <summary>
    /// Get books by description.
    /// </summary>
    /// <param name="description">The description of the book.</param>
    /// <returns>A list of books.</returns>
    [HttpGet("/description/{description?}")]
    [CheckBooksEmpty]
    [ProducesResponseType(typeof(IEnumerable<BookEntity>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByDescription(string? description = null)
    {
        var books = await _bookService.GetBooks(BookAttribute.Description, description);

        return Ok(books);
    }

    /// <summary>
    /// Get books by title.
    /// </summary>
    /// <param name="title">The title of the book.</param>
    /// <returns>A list of books.</returns>
    [HttpGet("/title/{title?}")]
    [CheckBooksEmpty]
    [ProducesResponseType(typeof(IEnumerable<BookEntity>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByTitle(string? title = null)
    {
        var books = await _bookService.GetBooks(BookAttribute.Title, title);

        return Ok(books);
    }

    /// <summary>
    /// Get books by genre.
    /// </summary>
    /// <param name="genre">The genre of the book.</param>
    /// <returns>A list of books.</returns>
    [HttpGet("/genre/{genre?}")]
    [CheckBooksEmpty]
    [ProducesResponseType(typeof(IEnumerable<BookEntity>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByGenre(string? genre = null)
    {
        var books = await _bookService.GetBooks(BookAttribute.Genre, genre);

        return Ok(books);
    }

    /// <summary>
    /// Get books by price range.
    /// </summary>
    /// <param name="minPrice">The minimum price of the book.</param>
    /// <param name="maxPrice">The maximum price of the book.</param>
    /// <returns>A list of books.</returns>
    [HttpGet("/price")]
    [CheckBooksEmpty]
    [ProducesResponseType(typeof(IEnumerable<BookEntity>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByPriceRange([FromQuery] double? minPrice, [FromQuery] double? maxPrice)
    {
        var validationResult = PriceHelper.ValidatePrices(minPrice, maxPrice);

        if (validationResult != null)
        {
            return BadRequest(validationResult);
        }

        var price = PriceHelper.GenerateValue(minPrice, maxPrice);

        var books = await _bookService.GetBooks(BookAttribute.Price, price);

        return Ok(books);
    }

    /// <summary>
    /// Get books by publish date.
    /// </summary>
    /// <param name="year">The year of the publish date.</param>
    /// <param name="month">The month of the publish date.</param>
    /// <param name="day">The day of the publish date.</param>
    /// <returns>A list of books.</returns>
    [HttpGet("/published/{year?}/{month?}/{day?}")]
    [CheckBooksEmpty]
    [ProducesResponseType(typeof(IEnumerable<BookEntity>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByPublishDate(int? year = null, int? month = null, int? day = null)
    {
        DateTime? parsedDate = PublishDateHelper.ParseDate(year, month, day);

        if (parsedDate == null)
        {
            return BadRequest("Invalid date provided.");
        }

        var books = await _bookService.GetBooks(BookAttribute.PublishDate, parsedDate.ToString());

        return Ok(books);
    }

    /// <summary>
    /// Update a book.
    /// </summary>
    /// <param name="id">The ID of the book to update.</param>
    /// <param name="book">The updated book data.</param>
    /// <returns>The updated book.</returns>
    [HttpPost("/{id}")]
    [ProducesResponseType(typeof(BookEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookEntity>> UpdateBook(string id, [FromBody] BookDto book)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest();
        }
        var updateBook = await _bookService.UpdateBook(id, book);

        if (updateBook is null)
        {
            return NotFound();
        }

        return Ok(updateBook);
    }

    /// <summary>
    /// Add a new book.
    /// </summary>
    /// <param name="book">The book to add.</param>
    /// <returns>The added book.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(BookEntity), StatusCodes.Status200OK)]
    public async Task<ActionResult<BookEntity>> AddBook(BookDto book)
    {
        var addedBook = await _bookService.CreateBook(book);

        return Ok(addedBook);
    }
}
