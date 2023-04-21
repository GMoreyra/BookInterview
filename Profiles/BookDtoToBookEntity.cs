using AutoMapper;
using Domain;
using DTOs;

namespace Profiles
{
    public class BookDtoToBookEntity : Profile
    {
        public BookDtoToBookEntity()
        {
            CreateMap<BookDto, BookEntity>();
        }
    }
}