using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop.Data;
using Book_Shop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Shop.Services.LogService
{
    public class LogService : ILogService
    {
        private readonly AppDbContext _db;

        public LogService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<AppLog>> GetAllLogs()
        {

            List<AppLog>  allLogs = await _db.AppLogs.ToListAsync();

            if (allLogs.Count > 0)
            {
                return allLogs;
            }
            return null;
        }
    }
}
