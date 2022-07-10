using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Book_Shop.Data;
using Book_Shop.Data.Models;
using Book_Shop.Dtos;

namespace Book_Shop.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public BookService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<ResponseMessage<BookDto>> AddBook(BookDto newBook)
        {
            ResponseMessage<BookDto> response = new ResponseMessage<BookDto>();
            try
            {
                Book book = new Book()
                {
                    Title = newBook.Title,
                    Description = newBook.Description,
                    IsRead = newBook.IsRead,
                    DateRead = newBook.IsRead ? newBook.DateRead.Value : new DateTime?(),
                    Rate = newBook.IsRead ? newBook.Rate.Value : (int?)null,
                    Genre = newBook.Genre,
                    Author = newBook.Author,
                    CoverUrl = newBook.CoverUrl,
                    CreatedAt = DateTime.UtcNow
                };
                await _db.Books.AddAsync(book);
                await _db.SaveChangesAsync();

                response.Data = null;
                response.IsSuccess = true;
                response.Message = "Book added successfully";


            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseMessage<IList<BookDto>>> GetAllBooks()
        {

            ResponseMessage<IList<BookDto>> response = new ResponseMessage<IList<BookDto>>();
            try
            {
                //Book book = _db.Books


            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public Task<ResponseMessage<BookDto>> GetBookById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
