using EFHomework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EFHomework
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();

            var connectionString = configuration.GetConnectionString("Books");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<BooksDbContext>();
            dbContextOptionsBuilder.UseSqlServer(connectionString);
            dbContextOptionsBuilder.LogTo(Console.WriteLine, minimumLevel: LogLevel.Information);

            using (var dbContext = new BooksDbContext(dbContextOptionsBuilder.Options))
            {
                //var novel = new Genre { Name = "Novel" };
                //var drama = new Genre { Name = "Drama" };

                //dbContext.Genres.AddRange(novel, drama);

                //var book1 = new Book { Name = "Frankenstein", Genre = novel };
                //var book2 = new Book { Name = "Romeo and Juliet", Genre = drama };

                //dbContext.Books.AddRange(book1, book2);

                //var author1 = new Author { Name = "William Shakespeare" };
                //var author2 = new Author { Name = "Paul Werstine" };

                //author1.Books.Add(book2);
                //author2.Books.Add(book2);

                //book2.Authors.Add(author1);
                //book2.Authors.Add(author2);

                //dbContext.Authors.AddRange(author1, author2);

                //var authorDetails1 = new AuthorDetails { Age = 52, Sex = Sex.Male, Author = author1 };
                //var authorDetails2 = new AuthorDetails { Age = 60, Sex = Sex.Male, Author = author2 };

                //dbContext.AuthorDetails.AddRange(authorDetails1, authorDetails2);

                //dbContext.SaveChanges();

                var book2 = await dbContext.Books.SingleAsync(i => i.Name == "Romeo and Juliet");

                book2.Name = "Romeo and Juliet 2";

                var book1 = await dbContext.Books.SingleAsync(i => i.Name == "Frankenstein");

                dbContext.Books.Remove(book1);

                dbContext.SaveChanges();
            }
        }
    }
}
