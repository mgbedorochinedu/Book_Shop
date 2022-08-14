using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop.Dtos.Author
{
    public class GetAuthorWithBooks
    {
        public string FullName { get; set; }
        public List<string> BookTitles { get; set; }
    }
}
