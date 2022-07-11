using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop.Data;
using Book_Shop.Data.Models;
using Book_Shop.Dtos;
using Book_Shop.Dtos.Book;

namespace Book_Shop.Services.BookService
{
    public interface IBookService
    {
        Task<ResponseMessage<BookDto>> AddBook(BookDto newBook);

        Task<ResponseMessage<List<BookDto>>> GetAllBooks();

        Task<ResponseMessage<BookDto>> GetBookById(int id);

        Task<ResponseMessage<UpdateBookDto>> UpdateBook(int id, UpdateBookDto updateBook);
    }
}
