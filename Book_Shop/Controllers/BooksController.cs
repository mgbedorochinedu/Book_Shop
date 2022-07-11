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

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        //Add Book
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBook([FromBody] BookDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _bookService.AddBook(request);
            return Ok(response);
        }

        //Get All Books
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBooks()
        {
            var response = await _bookService.GetAllBooks();
            return Ok(response);
        }

        //Get Book by Id
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBook(int id)
        {
            var response = await _bookService.GetBookById(id);
            if (response.Data == null || !response.IsSuccess)
                return NotFound(response);
            return Ok(response);
        }

        //Update Book
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

        //Delete Book
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
