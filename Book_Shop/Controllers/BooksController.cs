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
using Microsoft.Extensions.Logging;

namespace Book_Shop.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, IHttpContextAccessor httpContextAccessor, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
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
        public async Task<IActionResult> AddBookWithAuthors([FromBody] AddBookWithAuthorsDto request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(AddBookWithAuthors)} :- {ModelState} - {ModelState.IsValid}");
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBooks()
        {
            var response = await _bookService.GetAllBooks();
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        ///<summary>
        /// Get Book With Authors By Id
        ///</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBook(int id)
        {
            _logger.LogInformation($"Attempt in {nameof(GetBook)}");
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto request)
        {
            _logger.LogInformation($"Attempt in {nameof(UpdateBook)}");
            var response = await _bookService.UpdateBook(id, request);
            if (response.Data == null || !response.IsSuccess || id < 1)
            {
                _logger.LogError($"Invalid Update attempt in {nameof(UpdateBook)}");
                return BadRequest(response);
            }
            return Ok(response);
        }

        ///<summary>
        ///Delete Book
        ///</summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            _logger.LogInformation($"Attempt in {nameof(DeleteBook)}");
            var response = await _bookService.DeleteBook(id);
            if (response == null || !response.IsSuccess || response.Data == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteBook)}");
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
