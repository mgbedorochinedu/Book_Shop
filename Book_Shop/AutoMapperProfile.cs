using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Book_Shop.Data.Models;
using Book_Shop.Dtos;
using Book_Shop.Dtos.Author;
using Book_Shop.Dtos.Book;
using Book_Shop.Dtos.Publisher;

namespace Book_Shop
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, AddBookWithAuthorsDto>().ReverseMap();
            CreateMap<Book, UpdateBookDto>().ReverseMap();
            CreateMap<Book, GetBookWithAuthorsDto>().ReverseMap();
            CreateMap<Author, GetAuthorWithBooks>().ReverseMap();
            CreateMap<Publisher, GetPublisherWithBooksAndAuthorsDto>().ReverseMap();
            CreateMap<Publisher, PublisherDto>().ReverseMap();

        }


    }
}
