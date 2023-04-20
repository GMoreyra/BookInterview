using AutoMapper;
using Domain;
using DTOs;

namespace Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookDto, BookEntity>();
        }
    }
}