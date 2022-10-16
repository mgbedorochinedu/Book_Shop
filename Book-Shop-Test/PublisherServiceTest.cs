using System;
using System.Collections.Generic;
using Book_Shop.Data;
using Book_Shop.Data.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Book_Shop_Test
{
    public class PublisherServiceTest
    {

        private static readonly DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookShopDbTest")
            .Options;

        private AppDbContext _db;

        [SetUp]
        public void Setup()
        {
            _db = new AppDbContext(dbContextOptions);
            _db.Database.EnsureCreated();
        }



        [OneTimeTearDown]
        public void CleanUp()
        {
            _db.Database.EnsureDeleted();
        }



        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                new Publisher()
                {
                    Id = 1,
                    Name = "Publisher 1"
                },
                new Publisher()
                {
                    Id = 2,
                    Name = "Publisher 2"
                },
                new Publisher()
                {
                    Id = 3,
                    Name = "Publisher 3"
                }
            };
             _db.Publishers.AddRange(publishers);


            var authors = new List<Author>
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Author 2"
                }
            };
            _db.Authors.AddRange(authors);


            var books = new List<Book>
            {
                new Book()
                {
                    Id = 1,
                    Title = "Book 1 Title",
                    Description = "Book 1 Description",
                    IsRead = true,
                    DateRead = DateTime.Now,
                    Rate = 4,
                    Genre = "Genre",
                    CoverUrl = "https://google.com",
                    CreatedAt = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                    
                },
                new Book()
                {
                    Id = 2,
                    Title = "Book 2 Title",
                    Description = "Book 2 Description",
                    IsRead = true,
                    DateRead = DateTime.Now,
                    Rate = 4,
                    Genre = "Romance",
                    CoverUrl = "https://facebook.com",
                    CreatedAt = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                }
            };
            _db.Books.AddRange(books);


            var booksAuthors = new List<Book_Author>
            {
                new Book_Author()
                {
                    Id = 1,
                    BookId = 2,
                    AuthorId = 1
                },
                new Book_Author()
                {
                    Id = 2,
                    BookId = 1,
                    AuthorId = 1
                },
                new Book_Author()
                {
                    Id = 3,
                    BookId = 2,
                    AuthorId = 2
                }
            };
            _db.Books_Authors.AddRange(booksAuthors);

            _db.SaveChanges();
        }




    }
}