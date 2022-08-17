using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop.Dtos.Publisher
{
    public class GetPublisherWithBooksAndAuthorsDto
    {
        public string Name { get; set; }
        public List<BookAuthorDto> BookAuthors { get; set; }
    }


    public class BookAuthorDto
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; }
    }
}
