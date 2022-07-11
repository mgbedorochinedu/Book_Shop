using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Book_Shop.Data;
using Book_Shop.Data.Models;
using Book_Shop.Dtos;
using Book_Shop.Dtos.Book;
using Microsoft.EntityFrameworkCore;

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

        //AddBook
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

        //Get All Books
        public async Task<ResponseMessage<List<BookDto>>> GetAllBooks()
        {

            ResponseMessage<List<BookDto>> response = new ResponseMessage<List<BookDto>>();
            try
            {
                List<Book> book = await _db.Books.ToListAsync();
                if (book != null)
                {
                    response.Data = _mapper.Map<List<BookDto>>(book);
                    response.IsSuccess = true;
                    response.Message = "Successfully fetched all books";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Books not found";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        //Get Book By Id
        public async Task<ResponseMessage<BookDto>> GetBookById(int id)
        {
            ResponseMessage<BookDto> response = new ResponseMessage<BookDto>();
            try
            {
                Book book = await _db.Books.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (book != null)
                {
                    response.Data = _mapper.Map<BookDto>(book);
                    response.IsSuccess = true;
                    response.Message = "Successfully fetched all books";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Books not found";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        //Update Book
        public async Task<ResponseMessage<UpdateBookDto>> UpdateBook(int id, UpdateBookDto updateBook)
        {
            ResponseMessage<UpdateBookDto> response = new ResponseMessage<UpdateBookDto>();
            try
            {
                Book book = await _db.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (book != null)
                {
                    book.Title = updateBook.Title;
                    book.Description = updateBook.Description;
                    book.IsRead = updateBook.IsRead;
                    book.DateRead = updateBook.IsRead ? updateBook.DateRead.Value : new DateTime?();
                    book.Rate = updateBook.IsRead ? updateBook.Rate.Value : (int?) null;
                    book.Genre = updateBook.Genre;
                    book.Author = updateBook.Author;
                    book.CoverUrl = updateBook.CoverUrl;
                    book.ModifiedAt = DateTime.UtcNow;

                    _db.Books.Update(book);
                    await _db.SaveChangesAsync();

                    response.Data = _mapper.Map<UpdateBookDto>(book);
                    response.IsSuccess = true;
                    response.Message = "Book updated successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Book not found.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        //Delete Book
        public async Task<ResponseMessage<BookDto>> DeleteBook(int id)
        {
            ResponseMessage<BookDto> response = new ResponseMessage<BookDto>();
            try
            {
                Book book = await _db.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (book != null)
                {
                    _db.Books.Remove(book);
                    await _db.SaveChangesAsync();

                    response.Data = _mapper.Map<BookDto>(book);
                    response.IsSuccess = true;
                    response.Message = "Deleted successfully";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Book not found.";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
