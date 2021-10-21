using SimpleEntityFrameworkDemo.Data;
using SimpleEntityFrameworkDemo.Data.Entities;
using System;
using System.Threading.Tasks;

namespace SimpleEntityFrameworkDemo
{
    public class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("-------------RunUpdateCase Started--------------");
            await Try(RunUpdateCase());
            Console.WriteLine("-------------RunUpdateCase Finished-------------");
        }

        public static async Task RunUpdateCase()
        {
            var factory = new DemoDbContextFactory();
            var book = new Book
            {
                Id = Guid.NewGuid(),
                //TenantId = Guid.Parse("75e85884-0e27-4be1-9a1e-56fbcfccd8be"),
                Title = "Added Book",
                Author = new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Email = "test@test.com",
                    //TenantId = Guid.Parse("75e85884-0e27-4be1-9a1e-56fbcfccd8be"),
                }
            };

            using (var context = factory.CreateDbContext())
            {
                await context.Books.AddAsync(book);
                await context.SaveChangesAsync();
            }

            book.Author = null;
            using (var context = factory.CreateDbContext())
            {
                var attachedEntry = context.Books.Attach(book);
            }
        }

        public static async Task Try(Task task)
        {
            try
            {
                await task;
                Console.WriteLine("Success!");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
