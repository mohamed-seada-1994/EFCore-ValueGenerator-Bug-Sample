using SimpleEntityFrameworkDemo.Data;
using SimpleEntityFrameworkDemo.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleEntityFrameworkDemo
{
    public class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("---------------- ValueGeneration With (FK) Started ------------------");
            await Try(RunValueGenerationWithFK());
            Console.WriteLine("---------------- ValueGeneration With (FK) Finished -----------------");

            Console.WriteLine("----------- ValueGeneration Without (FK) Finished ------------");
            await Try(RunValueGenerationWithoutFK());
            Console.WriteLine("----------- ValueGeneration Without (FK) Finished ------------");
        }

        public static async Task RunValueGenerationWithFK()
        {
            var factory = new DemoDbContextFactory();
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Book",
                Author = new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Author",
                }
            };

            using (var context = factory.CreateDbContext())
            {
                await context.Books.AddAsync(book);
                await context.SaveChangesAsync();

            }

            book.TenantId = default;
            using (var context = factory.CreateDbContext())
            {
                var attachedEntry = context.Books.Attach(book);

                Console.WriteLine($"Expected: Unchanged, Actual: {attachedEntry.State}");
            }
        }

        public static async Task RunValueGenerationWithoutFK()
        {
            var factory = new DemoDbContextFactory();
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = "Student",
            };

            using (var context = factory.CreateDbContext())
            {
                await context.Students.AddAsync(student);
                await context.SaveChangesAsync();
            }

            student.TenantId = default;
            using (var context = factory.CreateDbContext())
            {
                var attachedEntry = context.Students.Attach(student);

                Console.WriteLine($"Expected: Unchanged, Actual: {attachedEntry.State}");
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
