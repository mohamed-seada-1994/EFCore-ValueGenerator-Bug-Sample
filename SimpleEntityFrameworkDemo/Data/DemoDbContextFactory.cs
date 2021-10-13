using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SimpleEntityFrameworkDemo.Data
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class DemoDbContextFactory : IDesignTimeDbContextFactory<DemoDbContext>, IDbContextFactory<DemoDbContext>
    {
        public DemoDbContext CreateDbContext()
            => CreateDbContext(default);

        public DemoDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();
            Console.WriteLine(configuration.GetConnectionString("Default"));
            var builder = new DbContextOptionsBuilder<DemoDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new DemoDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
