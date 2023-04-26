using Application;
using AutoMapper;
using BookInterview.Controllers;
using Domain;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using static Utils.BookAttributeEnum;

namespace Tests
{
    public class BooksControllerTests
    {
        private readonly Mock<IBookService> _bookServiceMock;
        private readonly IMapper _mapper;
        private readonly BooksController _booksController;

        public BooksControllerTests()
        {
            _bookServiceMock = new Mock<IBookService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BookDto, BookEntity>();
            });

            _mapper = mapperConfig.CreateMapper();

            _booksController = new BooksController(_bookServiceMock.Object, _mapper);
        }

        [Fact]
        public async Task GetBooks_ReturnsAllBooks()
        {
            var expectedBooks = FakeData.GetFakeBooks();
            _bookServiceMock.Setup(service => service.GetBooks(BookAttribute.None, null)).ReturnsAsync(expectedBooks);

            var result = await _booksController.GetBooks();

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            (okResult?.Value ?? Enumerable.Empty<BookEntity>()).Should().BeEquivalentTo(expectedBooks);
        }

        [Fact]
        public async Task UpdateBook_ValidBook_ReturnsUpdatedBook()
        {
            var bookToUpdate = FakeData.GetFakeBooks()[0];
            var bookDto = CreateBookDtoFromBookEntity(bookToUpdate);

            var updatedBook = new BookEntity { Id = "B-1", Author = "Updated Author", Title = "Updated Title" };
            _bookServiceMock.Setup(service => service.UpdateBook(It.IsAny<BookEntity>())).ReturnsAsync(updatedBook);

            var result = await _booksController.UpdateBook(bookToUpdate.Id, bookDto);

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            (okResult?.Value ?? Enumerable.Empty<BookEntity>()).Should().BeEquivalentTo(updatedBook);
        }

        [Fact]
        public async Task AddBook_ValidBook_ReturnsAddedBook()
        {
            var newBook = FakeData.GetFakeBooks()[0];
            var bookDto = CreateBookDtoFromBookEntity(newBook);

            _bookServiceMock.Setup(service => service.AddBook(It.IsAny<BookEntity>())).ReturnsAsync(newBook);

            var result = await _booksController.AddBook(bookDto);

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            (okResult?.Value ?? Enumerable.Empty<BookEntity>()).Should().BeEquivalentTo(newBook);
        }

        private static BookDto CreateBookDtoFromBookEntity(BookEntity bookEntity)
        {
            return new BookDto
            {
                Author = bookEntity.Author,
                Title = bookEntity.Title,
                Genre = bookEntity.Genre,
                Price = bookEntity.Price,
                Description = bookEntity.Description,
                PublishDate = bookEntity.PublishDate
            };
        }
    }
}
