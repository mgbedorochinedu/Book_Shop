﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop.Data.Models
{
    public class AppLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; } //XML Properties
        public string LogEvent { get; set; }
    }
}
