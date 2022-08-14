using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop.Dtos.Author;
using Book_Shop.Services.AuthorService;

namespace Book_Shop.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        ///<summary>
        /// Add Author
        ///</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _authorService.AddAuthor(request);
            return Ok(response);
        }

        ///<summary>
        /// Get Author With Books By Id
        ///</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAuthorWithBooks(int id)
        {
            var response = await _authorService.GetAuthorWithBooks(id);
            if (response.Data == null || !response.IsSuccess || id < 1)
                return NotFound(response);
            return Ok(response);
        }


    }
}
