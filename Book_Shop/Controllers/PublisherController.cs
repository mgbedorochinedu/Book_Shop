using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop.Dtos.Publisher;
using Book_Shop.Services.PublisherService;

namespace Book_Shop.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }


        ///<summary>
        ///Add Publisher
        ///</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPublisher([FromBody] AddPublisherDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _publisherService.AddPublisher(request);
            return Created(nameof(AddPublisher), response);
        }

        ///<summary>
        /// Get Publisher with Books and Authors by Id
        ///</summary>
        [HttpGet("get-publisher-with-books-and-authors/{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PublisherWithBooksAndAuthors(int id)
        {
            var response = await _publisherService.GetPublisherWithBooksAndAuthors(id);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        ///<summary>
        /// Get Publisher by Id
        ///</summary>
        [HttpGet("get-publisher-by-id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPublisherById(int id)
        {
            var response = await _publisherService.GetPublisherById(id);
            if (response == null)
                return NotFound();
            return Ok(response);
        }


        ///<summary>
        /// Delete Publisher
        ///</summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var response = await _publisherService.DeletePublisher(id);
            if (response == null) return BadRequest();
            return Ok(response);
        }


    }
}
