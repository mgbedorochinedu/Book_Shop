using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Book_Shop.Data;
using Book_Shop.Data.Models;
using Book_Shop.Dtos.Publisher;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
        public async Task<MessageResponse<AddPublisherDto>> AddPublisher(AddPublisherDto newPublisher)
        {
            MessageResponse<AddPublisherDto> response = new MessageResponse<AddPublisherDto>();

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
                response.Message = "Something Went Wrong: Internal Server Error. Please Try Again Later.";
                Log.Error($"Something went wrong : {ex.Message}");
            }

            return response;
        }

        ///<summary>
        /// Get Publisher with Books and Authors by Id
        ///</summary>
        public async Task<MessageResponse<GetPublisherWithBooksAndAuthorsDto>> GetPublisherWithBooksAndAuthors(int id)
        {
            MessageResponse<GetPublisherWithBooksAndAuthorsDto> response = new MessageResponse<GetPublisherWithBooksAndAuthorsDto>();
            try
            {
                var dbPublisher = await _db.Publishers.Where(x => x.Id.Equals(id)).Select(publisher =>
                    new GetPublisherWithBooksAndAuthorsDto()
                    {
                        Name =  publisher.Name,
                        BookAuthors = publisher.Books.Select(n => new BookAuthorDto()
                        {
                            BookName = n.Title,
                            BookAuthors = n.Book_Authors.Select(a => a.Author.FullName).ToList()
                            
                        }).ToList(),
                        
                    }).FirstOrDefaultAsync();
                if (dbPublisher != null)
                {
                    response.Data = _mapper.Map<GetPublisherWithBooksAndAuthorsDto>(dbPublisher);
                    response.IsSuccess = true;
                    response.Message = "Publisher with Books and Authors fetched successful.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = $"No Publisher record with id: {id} found.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something Went Wrong: Internal Server Error. Please Try Again Later.";
                Log.Error($"Something went wrong : {ex.Message}");
            }
            return response;
        }



        ///<summary>
        ///Get Publisher By Id
        ///</summary>
        public async Task<MessageResponse<PublisherDto>> GetPublisherById(int id)
        {
            MessageResponse<PublisherDto> response = new MessageResponse<PublisherDto>();
            try
            {
                Publisher dbPublisher = await _db.Publishers.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (dbPublisher != null)
                {
                    response.Data = _mapper.Map<PublisherDto>(dbPublisher);
                    response.IsSuccess = true;
                    response.Message = "Publisher fetched successful.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = $"Publisher with id: {id} not found.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something Went Wrong: Internal Server Error. Please Try Again Later.";
                Log.Error($"Something went wrong : {ex.Message}");
            }

            return response;
        }


        ///<summary>
        ///Delete Publisher
        ///</summary>
        public async Task<MessageResponse<PublisherDto>> DeletePublisher(int id)
        {
            MessageResponse<PublisherDto> response = new MessageResponse<PublisherDto>();
            try
            {
                Publisher dbPublisher = await _db.Publishers.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (dbPublisher != null)
                {
                     _db.Publishers.Remove(dbPublisher);
                     await _db.SaveChangesAsync();
                    response.Data = _mapper.Map<PublisherDto>(dbPublisher);
                    response.IsSuccess = true;
                    response.Message = "Deleted Publisher";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = $"No Publisher with id: {id} found.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something Went Wrong: Internal Server Error. Please Try Again Later.";
                Log.Error($"Something went wrong : {ex.Message}");
            }

            return response;
        }

        public async Task<MessageResponse<List<PublisherDto>>> GetAllPublishers(string sortBy)
        {
            MessageResponse<List<PublisherDto>> response = new MessageResponse<List<PublisherDto>>();

            try
            {
                List<Publisher> dbPublisher = await _db.Publishers.ToListAsync();
                if (!string.IsNullOrEmpty(sortBy))
                {
                    switch (sortBy)
                    {
                        case "name_desc":
                            dbPublisher = dbPublisher.OrderByDescending(n => n.Name).ToList();
                            break;
                        default:
                            break;
                    }
                }

                if (dbPublisher == null || dbPublisher.Count < 0)
                {
                    response.IsSuccess = false;
                    response.Message = "No Publisher found";
                    response.Data = null;
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "Publishers fetched successful";
                    response.Data = _mapper.Map<List<PublisherDto>>(dbPublisher);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something Went Wrong: Internal Server Error. Please Try Again Later.";
                Log.Error($"Something went wrong : {ex.Message}");
            }
            return response;

        }

    }
}
