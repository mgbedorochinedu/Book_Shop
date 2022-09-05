using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop.Data.Models;

namespace Book_Shop.Services.LogService
{
    public interface ILogService
    {
        Task<List<AppLog>> GetAllLogs();
    }
}
