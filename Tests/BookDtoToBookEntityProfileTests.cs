using AutoMapper;
using Domain;
using DTOs;
using Mapper;

namespace Tests
{
    public class BookDtoToBookEntityProfileTests
    {
        private readonly IMapper _mapper;

        public BookDtoToBookEntityProfileTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BookDtoToBookEntityProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public void BookDtoToBookEntityMapping_ShouldBeValid()
        {
            var bookDto = new BookDto
            {
                Author = "Author 1",
                Title = "Title 1",
                Genre = "Genre 1",
                Price = 10,
                Description = "Test description 1",
                PublishDate = new DateTime(2021, 1, 1)
            };

            var bookEntity = _mapper.Map<BookEntity>(bookDto);

            bookEntity.Author.Should().Be(bookDto.Author);
            bookEntity.Title.Should().Be(bookDto.Title);
            bookEntity.Genre.Should().Be(bookDto.Genre);
            bookEntity.Price.Should().Be(bookDto.Price);
            bookEntity.Description.Should().Be(bookDto.Description);
            bookEntity.PublishDate.Should().Be(bookDto.PublishDate);
        }
    }
}
