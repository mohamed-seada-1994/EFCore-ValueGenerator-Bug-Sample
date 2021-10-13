using SimpleEntityFrameworkDemo.Data;
using SimpleEntityFrameworkDemo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleEntityFrameworkDemo
{
    public class Program
    {
        public static async Task Main()
        {
            var factory = new DemoDbContextFactory();
            using var context = factory.CreateDbContext();

            //var books = new List<Book>
            //{
            //    new Book
            //    {
            //        TenantId = Guid.NewGuid(),
            //        Title = "test 1",
            //        Author = new Author(),
            //    },
            //    new Book
            //    {
            //        TenantId = Guid.Parse("75e85884-0e27-4be1-9a1e-56fbcfccd8be"),
            //        Title = "test 2",
            //        Author = new Author
            //        {
            //            TenantId = Guid.Parse("564a12ec-a20a-49d9-8c00-2eb17dd70fc9"),
            //        }
            //    },
            //};
            //await context.Books.AddRangeAsync(books);

            var book = new Book
            {
                //Id = new Guid(),
                //TenantId = Guid.NewGuid(),
                Title = "test 4",
                //Author = new Author(),
            };

            var state = await context.Books.AddAsync(book);

            await context.SaveChangesAsync();
        }
    }
}
