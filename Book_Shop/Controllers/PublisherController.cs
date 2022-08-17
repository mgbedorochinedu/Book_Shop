﻿using Microsoft.AspNetCore.Http;
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
            return Ok(response);
        }

        ///<summary>
        /// Get Publisher with Books and Authors by Id
        ///</summary>
        [HttpGet]
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


    }
}
