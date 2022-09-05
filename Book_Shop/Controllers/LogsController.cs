using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop.Services.LogService;

namespace Book_Shop.Controllers
{
    [Route("api/logs")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogs()
        {
            try
            {
                var response = await _logService.GetAllLogs();
                return Ok(response);
            }
            catch (Exception)
            {
               return BadRequest("Could not loan logs from the database.");
            }
        }
    }
}
