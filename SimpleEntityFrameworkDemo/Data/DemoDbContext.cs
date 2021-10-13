using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleEntityFrameworkDemo.Data.Entities;

namespace SimpleEntityFrameworkDemo.Data
{
    public class DemoDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public DemoDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
                .ToList();

            //((Book)changedEntities[0].Entity).TenantId = Guid.NewGuid();
            //((Book)changedEntities[1].Entity).TenantId = Guid.NewGuid();
            var a1 = changedEntities[0];
            var a2 = changedEntities[1];
            var e1 = changedEntities[0].Property("TenantId");
            var e2 = changedEntities[1].Property("TenantId");

            //a1.DetectChanges();
            //e1.CurrentValue = e1.CurrentValue;

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
