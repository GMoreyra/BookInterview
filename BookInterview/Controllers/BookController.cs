using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;
using static Utils.EnumHelper;

namespace BookInterview.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooks()
        {
            var books = await _bookService.GetBooks(BookAttribute.None, null);

            return Ok(books);
        }

        [HttpGet]
        [Route("/id/{value?}")]
        public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksById(string? value = null)
        {
            var books = await _bookService.GetBooks(BookAttribute.Id, value);

            return Ok(books);
        }

        [HttpGet]
        [Route("/author/{value?}")]
        public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByAuthor(string? value = null)
        {
            var books = await _bookService.GetBooks(BookAttribute.Author, value);

            return Ok(books);
        }

        [HttpGet]
        [Route("/description/{value?}")]
        public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByDescription(string? value = null)
        {
            var books = await _bookService.GetBooks(BookAttribute.Description, value);

            return Ok(books);
        }

        [HttpGet]
        [Route("/title/{value?}")]
        public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByTitle(string? value = null)
        {
            var books = await _bookService.GetBooks(BookAttribute.Title, value);

            return Ok(books);
        }

        [HttpGet]
        [Route("/genre/{value?}")]
        public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByGenre(string? value = null)
        {
            var books = await _bookService.GetBooks(BookAttribute.Genre, value);

            return Ok(books);
        }

        [HttpGet]
        [Route("price")]
        public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByPriceRange([FromQuery] double? minPrice, [FromQuery] double? maxPrice)
        {
            if (minPrice.HasValue && minPrice < 0)
            {
                return BadRequest("Minimum price must be positive.");
            }

            if (maxPrice.HasValue && maxPrice < 0)
            {
                return BadRequest("Maximum price must be positive.");
            }

            if (minPrice.HasValue && maxPrice.HasValue && minPrice > maxPrice)
            {
                return BadRequest("Minimum price cannot be greater than maximum price.");
            }

            string? value = null;

            if (minPrice.HasValue && !maxPrice.HasValue)
            {
                value = minPrice.Value.ToString();
            }

            if (minPrice.HasValue && maxPrice.HasValue)
            {
                value = $"{minPrice.Value}&{maxPrice.Value}";
            }

            var books = await _bookService.GetBooks(BookAttribute.Price, value);

            return Ok(books);
        }

        [HttpGet]
        [Route("/published/{value?}")]
        public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByPublishDate(string? value = null)
        {
            var books = await _bookService.GetBooks(BookAttribute.PublishDate, value);

            return Ok(books);
        }

        //[HttpPost]
        //public async Task<ActionResult<BookEntity>> AddBook(BookEntity book)
        //{
        //    var addedBook = await _bookRepository.AddBoOk(books);
        //    return CreatedAtAction(nameof(GetBook), new { id = addedBook.Id }, addedBook);
        //}
    }
}