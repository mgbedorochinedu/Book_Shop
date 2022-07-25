﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Book_Shop.Data;
using Book_Shop.Data.Models;
using Book_Shop.Dtos.Publisher;

namespace Book_Shop.Services.PublisherService
{
    public class PublisherService : IPublisherService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public PublisherService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResponseMessage<PublisherDto>> AddPublisher(PublisherDto newPublisher)
        {
            ResponseMessage<PublisherDto> response = new ResponseMessage<PublisherDto>();

            try
            {
                Publisher publisher = new Publisher()
                {
                    Name = newPublisher.Name
                };
                await _db.Publishers.AddAsync(publisher);
                await _db.SaveChangesAsync();

                response.Data = null;
                response.IsSuccess = true;
                response.Message = "Publisher added successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
            }

            return response;
        }
    }
}