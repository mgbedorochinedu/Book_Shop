using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Book_Shop.Data;
using Book_Shop.Data.Models;
using Book_Shop.Dtos.Author;

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

        public async Task<MessageResponse<AuthorDto>> AddAuthor(AuthorDto newAuthor)
        {
            MessageResponse<AuthorDto> response = new MessageResponse<AuthorDto>();

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
                response.Message = $"Something went wrong on {ex.Message}";
            }

            return response;

        }
    }
}

