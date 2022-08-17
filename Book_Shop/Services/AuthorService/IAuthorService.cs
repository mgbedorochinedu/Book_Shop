using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop.Data;
using Book_Shop.Dtos.Author;

namespace Book_Shop.Services.AuthorService
{
    public interface IAuthorService
    {
        Task<MessageResponse<AddAuthorDto>> AddAuthor(AddAuthorDto newAuthor);

        Task<MessageResponse<GetAuthorWithBooks>> GetAuthorWithBooks(int id);
    }
}
