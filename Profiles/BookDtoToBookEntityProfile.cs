using AutoMapper;
using Domain;
using DTOs;

namespace Mapper
{
    public class BookDtoToBookEntityProfile : Profile
    {
        public BookDtoToBookEntityProfile()
        {
            CreateMap<BookDto, BookEntity>();
        }
    }
}