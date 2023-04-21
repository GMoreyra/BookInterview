using Application;
using AutoMapper;
using Domain;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Utils;
using static Utils.BookAttributeEnum;

namespace BookInterview.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooks()
        {
            var books = await _bookService.GetBooks(BookAttribute.None, null);

            return Ok(books);
        }

        [HttpGet]
        [Route("/id/{id?}")]
        public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksById(string? id = null)
        {
            var books = await _bookService.GetBooks(BookAttribute.Id, id);

            return Ok(books);
        }

        [HttpGet]
        [Route("/author/{author?}")]
        public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByAuthor(string? author = null)
        {
            var books = await _bookService.GetBooks(BookAttribute.Author, author);

            return Ok(books);
        }

        [HttpGet]
        [Route("/description/{description?}")]
        public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByDescription(string? description = null)
        {
            var books = await _bookService.GetBooks(BookAttribute.Description, description);

            return Ok(books);
        }

        [HttpGet]
        [Route("/title/{title?}")]
        public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByTitle(string? title = null)
        {
            var books = await _bookService.GetBooks(BookAttribute.Title, title);

            return Ok(books);
        }

        [HttpGet]
        [Route("/genre/{genre?}")]
        public async Task<ActionResult<IEnumerable<BookEntity>>> GetBooksByGenre(string? genre = null)
        {
            var books = await _bookService.GetBooks(BookAttribute.Genre, genre);

            return Ok(books);
        }

        [HttpGet]
        [Route("/price")]
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

        [HttpGet]
        [Route("/published/{year?}/{month?}/{day?}")]
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

        [HttpPost]
        [Route("/{id}")]
        public async Task<ActionResult<BookEntity>> UpdateBook(int id, [FromBody] BookDto book)
        {
            var bookEntity = _mapper.Map<BookEntity>(book);
            bookEntity.Id = id;

            var updateBook = await _bookService.UpdateBook(bookEntity);

            if (updateBook is null)
            {
                return NotFound();
            }

            return Ok(updateBook);
        }

        [HttpPut]
        public async Task<ActionResult<BookEntity>> AddBook(BookDto book)
        {
            var bookEntity = _mapper.Map<BookEntity>(book);
            var addedBook = await _bookService.AddBook(bookEntity);

            return Ok(addedBook);
        }
    }
}