using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop.Data;
using Book_Shop.Dtos.Publisher;

namespace Book_Shop.Services.PublisherService
{
    public interface IPublisherService
    {
        Task<MessageResponse<AddPublisherDto>> AddPublisher(AddPublisherDto newPublisher);

        Task<MessageResponse<GetPublisherWithBooksAndAuthorsDto>> GetPublisherWithBooksAndAuthors(int id);

        Task<MessageResponse<PublisherDto>> GetPublisherById(int id);

        Task<MessageResponse<PublisherDto>> DeletePublisher(int id);

        Task<MessageResponse<List<PublisherDto>>> GetAllPublishers(string sortBy, string searchString);

    }
}
