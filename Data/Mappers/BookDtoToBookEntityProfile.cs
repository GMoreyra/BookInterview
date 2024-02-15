using AutoMapper;
using Data.Entities;

namespace Data.Mappers;

public class BookDtoToBookEntityProfile : Profile
{
    public BookDtoToBookEntityProfile()
    {
        CreateMap<BookDto, BookEntity>();
    }
}

public class BookDto
{
    public string? Author { get; set; }

    public string? Description { get; set; }

    public string? Title { get; set; }

    public string? Genre { get; set; }

    public double? Price { get; set; }

    public DateTime? PublishDate { get; set; }
}