using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Book_Shop.Data
{
    public class AppDbInitializer
    {
        public static void DataSeeding(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (!context.Books.Any())
                {
                    context.AddRange(new Book()
                    {
                        Title = "1st Book Title",
                        Description = "First Book Description",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate = 4,
                        Genre = "Biography",
                        Author = "First Author",
                        CoverUrl = "https://wwww.mybookshop.com",
                        CreatedAt = DateTime.UtcNow

                    });
                    context.AddRange(new Book()
                    {
                        Title = "2nd Book Title",
                        Description = "Second Book Description",
                        IsRead = false,
                        Genre = "Biography",
                        Author = "Second Author",
                        CoverUrl = "https://wwww.mysecondbookshop.com",
                        CreatedAt = DateTime.UtcNow
                    });
                    context.SaveChanges();

                }
            }
        }
    }
}
