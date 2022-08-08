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
        public async Task<MessageResponse<BookDto>> AddBookWithAuthors(BookDto newBook)
        {
            MessageResponse<BookDto> response = new MessageResponse<BookDto>();
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
                    CoverUrl = newBook.CoverUrl,
                    PublisherId = newBook.PublisherId,
                    CreatedAt = DateTime.UtcNow
                };
                await _db.Books.AddAsync(book);
                await _db.SaveChangesAsync();

                foreach (var id in newBook.AuthorIds)
                {
                    var bookAuthor = new Book_Author()
                    {
                        BookId = book.Id,
                        AuthorId = id
                    };
                    await _db.Books_Authors.AddAsync(bookAuthor);
                    await _db.SaveChangesAsync();
                }

                response.Data = null;
                response.IsSuccess = true;
                response.Message = "Added Book with Authors successfully";

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Something went wrong on {ex.Message}";
            }

            return response;
        }

        //Get All Books
        public async Task<MessageResponse<List<BookDto>>> GetAllBooks()
        {

            MessageResponse<List<BookDto>> response = new MessageResponse<List<BookDto>>();
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
        public async Task<MessageResponse<BookDto>> GetBookById(int id)
        {
            MessageResponse<BookDto> response = new MessageResponse<BookDto>();
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
                response.Message = $"Something went wrong on {ex.Message}";
            }

            return response;
        }

        //Update Book
        public async Task<MessageResponse<UpdateBookDto>> UpdateBook(int id, UpdateBookDto updateBook)
        {
            MessageResponse<UpdateBookDto> response = new MessageResponse<UpdateBookDto>();
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
                response.Message = $"Something went wrong on {ex.Message}";
            }

            return response;
        }

        //Delete Book
        public async Task<MessageResponse<BookDto>> DeleteBook(int id)
        {
            MessageResponse<BookDto> response = new MessageResponse<BookDto>();
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
                response.Message = $"Something went wrong on {ex.Message}";
            }

            return response;
        }

    }
}
