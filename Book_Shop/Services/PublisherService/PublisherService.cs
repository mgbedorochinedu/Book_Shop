using System;
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

        ///<summary>
        /// Add Publisher
        ///</summary>
        public async Task<MessageResponse<PublisherDto>> AddPublisher(PublisherDto newPublisher)
        {
            MessageResponse<PublisherDto> response = new MessageResponse<PublisherDto>();

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
                response.Message = $"Something went wrong : {ex.Message}";
            }

            return response;
        }
    }
}
