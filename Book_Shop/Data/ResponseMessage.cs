using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop.Data
{
    public class ResponseMessage<T>
    {
        public T Data { get; set; }
        public string Message { get; set; } = null;
        public bool IsSuccess { get; set; } = true;
    }
}
