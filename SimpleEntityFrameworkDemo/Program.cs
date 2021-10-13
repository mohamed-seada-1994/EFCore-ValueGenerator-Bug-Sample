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
            Console.WriteLine("-------------RunSuccessCase Started-------------");
            await Try(RunSuccessCase());
            Console.WriteLine("-------------RunSuccessCase Finished------------");
            
            Console.WriteLine("-------------RunFailedCase Started--------------");
            await Try(RunFailedCase());
            Console.WriteLine("-------------RunFailedCase Finished-------------");
        }

        public static async Task RunSuccessCase()
        {
            var factory = new DemoDbContextFactory();
            using var context = factory.CreateDbContext();


            var book = new Book
            {
                TenantId = Guid.Parse("75e85884-0e27-4be1-9a1e-56fbcfccd8be").ToString(),
                Title = "Success Case",
            };

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
        }

        public static async Task RunFailedCase()
        {
            var factory = new DemoDbContextFactory();
            using var context = factory.CreateDbContext();


            var book = new Book
            {
                Title = "Success Case",
            };

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
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
