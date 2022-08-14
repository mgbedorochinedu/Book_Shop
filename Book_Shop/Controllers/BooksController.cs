using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop.Data;
using Book_Shop.Dtos;
using Book_Shop.Dtos.Book;
using Book_Shop.Services.BookService;

namespace Book_Shop.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BooksController(IBookService bookService, IHttpContextAccessor httpContextAccessor)
        {
            _bookService = bookService;
            _httpContextAccessor = httpContextAccessor;
        }

        //IP Address
        [HttpGet("ip-address")]
        public IActionResult Index()
        {
            var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            return Content(ip);
        }

        ///<summary>
        ///Add Book with Authors
        ///</summary>
        [HttpPost("add-book-with-authors")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBookWithAuthors([FromBody] BookDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _bookService.AddBookWithAuthors(request);
            return Ok(response);
        }

        ///<summary>
        ///Get All Books
        ///</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBooks()
        {
            var response = await _bookService.GetAllBooks();
            return Ok(response);
        }

        ///<summary>
        /// Get Book With Authors By Id
        ///</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBook(int id)
        {
            var response = await _bookService.GetBookWithAuthorsById(id);
            if (response.Data == null || !response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }

        ///<summary>
        /// Update Book
        ///</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto request)
        {
            var response = await _bookService.UpdateBook(id, request);
            if (response.Data == null || !response.IsSuccess || id < 1)
                return BadRequest(response);
            return Ok(response);
        }

        ///<summary>
        ///Delete Book
        ///</summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var response = await _bookService.DeleteBook(id);
            if (response == null || !response.IsSuccess || response.Data == null)
                return NotFound(response);
            return Ok(response);
        }
    }
}
