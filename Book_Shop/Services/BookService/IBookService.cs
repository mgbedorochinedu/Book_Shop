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
        Task<MessageResponse<BookDto>> AddBookWithAuthors(AddBookWithAuthorsDto newBook);

        Task<MessageResponse<List<BookDto>>> GetAllBooks();

        Task<MessageResponse<GetBookWithAuthorsDto>> GetBookWithAuthorsById(int id);

        Task<MessageResponse<UpdateBookDto>> UpdateBook(int id, UpdateBookDto updateBook);

        Task<MessageResponse<BookDto>> DeleteBook(int id);
    }
}
