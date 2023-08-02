

using AutoMapper;
using Book.Core.Entities;
using Book.Service.Dtos.Books;


namespace Book.Service.Profiles.Bookss
{
    public class BookProfile:Profile
    {
        public BookProfile()
        {
            CreateMap<BookPostDto, Books>();
            CreateMap<BookUpdateDto, Books>();
            CreateMap<Books, BookGetDto>();
        }
    }
}
