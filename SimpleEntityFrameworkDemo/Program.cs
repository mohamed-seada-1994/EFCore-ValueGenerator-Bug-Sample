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
            Console.WriteLine("---------------- ValueGeneration With Simple (FK) Started ------------------");
            await Try(RunValueGenerationWithSimpleFK());
            Console.WriteLine("---------------- ValueGeneration With Simple (FK) Finished -----------------");

            Console.WriteLine("---------------- ValueGeneration With Generated (FK) Started ------------------");
            await Try(RunValueGenerationWithGeneratedFK());
            Console.WriteLine("---------------- ValueGeneration With Generated (FK) Finished -----------------");

            Console.WriteLine("----------- ValueGeneration Without (FK) Finished ------------");
            await Try(RunValueGenerationWithoutFK());
            Console.WriteLine("----------- ValueGeneration Without (FK) Finished ------------");
        }

        public static async Task RunValueGenerationWithSimpleFK()
        {
            var factory = new DemoDbContextFactory();
            var userClaim = new UserClaim
            {
                Id = Guid.NewGuid(),
                Claim = "Role",
                Value = "Member",
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "user@domain.name",
                }
            };

            using (var context = factory.CreateDbContext())
            {
                await context.UserClaims.AddAsync(userClaim);
                await context.SaveChangesAsync();

            }

            userClaim.User = null;
            userClaim.TenantId = default;
            using (var context = factory.CreateDbContext())
            {
                var attachedEntry = context.UserClaims.Attach(userClaim);

                Console.WriteLine($"Expected: Unchanged, Actual: {attachedEntry.State}");
            }
        }

        public static async Task RunValueGenerationWithGeneratedFK()
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

            book.Author = null;
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
                // Console.WriteLine("Success!");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
