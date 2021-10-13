using System.Reflection;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleEntityFrameworkDemo.Data.Entities;

namespace SimpleEntityFrameworkDemo.Data
{
    public class DemoDbContext : MultiTenantDbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public DemoDbContext(ITenantInfo tenantInfo, DbContextOptions options)
            : base(tenantInfo, options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var booksEntity = modelBuilder.Entity<Book>();
            booksEntity.IsMultiTenant()
                .AdjustIndexes()
                .AdjustKey(booksEntity.Metadata.FindPrimaryKey(), modelBuilder);

            var authorEntity = modelBuilder.Entity<Author>();
            authorEntity.IsMultiTenant()
                .AdjustIndexes()
                .AdjustKey(authorEntity.Metadata.FindPrimaryKey(), modelBuilder);

            modelBuilder.ConfigureMultiTenant();
        }
    }
}
