using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop.Dtos.Book
{
    public class AddBookWithAuthorsDto
    {
        [Required(ErrorMessage = "Book Title is Required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string CoverUrl { get; set; }
        [Required(ErrorMessage = "Book Publisher is Required")]
        public int PublisherId { get; set; }
        [Required(ErrorMessage = "Book Author is Required")]
        public List<int> AuthorIds { get; set; }
    }
}
