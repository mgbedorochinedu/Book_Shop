using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Book_Shop.Data;
using Book_Shop.Data.Models;
using Book_Shop.Dtos.Author;
using Microsoft.EntityFrameworkCore;

namespace Book_Shop.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public AuthorService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        ///<summary>
        /// Add Author
        ///</summary>
        public async Task<MessageResponse<AddAuthorDto>> AddAuthor(AddAuthorDto newAuthor)
        {
            MessageResponse<AddAuthorDto> response = new MessageResponse<AddAuthorDto>();

            try
            {
                Author author = new Author()
                {
                    FullName = newAuthor.FullName
                };
                await _db.Authors.AddAsync(author);
                await _db.SaveChangesAsync();

                response.Data = null;
                response.IsSuccess = true;
                response.Message = "Author added successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Something went wrong : {ex.Message}";
            }

            return response;
        }

        ///<summary>
        /// Get Author With Books By Id
        ///</summary>
        public async Task<MessageResponse<GetAuthorWithBooks>> GetAuthorWithBooks(int id)
        {
            MessageResponse<GetAuthorWithBooks> response = new MessageResponse<GetAuthorWithBooks>();
            try
            {
                var dbAuthor = await _db.Authors
                    .Where(x => x.Id == id).Select(author => new GetAuthorWithBooks()
                {
                    FullName = author.FullName,
                    BookTitles = author.Book_Authors.Select(b => b.Book.Title).ToList()
                }).FirstOrDefaultAsync();

                if (dbAuthor != null)
                {
                    response.Data = _mapper.Map<GetAuthorWithBooks>(dbAuthor);
                    response.IsSuccess = true;
                    response.Message = "Successfully fetch Author with Books";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = $"No Author with id: {id} found";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Something went wrong : {ex.Message}";
            }

            return response;
        }
    }
}

