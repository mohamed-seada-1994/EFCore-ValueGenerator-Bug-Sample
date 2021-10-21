using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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


            var IsKeyUnknown = (typeof(EntityEntry).GetProperty("InternalEntry", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(changedEntities[0]) as InternalEntityEntry).IsKeyUnknown;
            Console.WriteLine($"IsKeyUnknown: {IsKeyUnknown}");

            var addedEntities = ChangeTracker.Entries()
                .Where(e => e.State is EntityState.Added);

            foreach (var entity in addedEntities)
            {
                var tenantIdProperty = entity.Property("TenantId");
                tenantIdProperty.CurrentValue = tenantIdProperty.CurrentValue;
            }


            IsKeyUnknown = (typeof(EntityEntry).GetProperty("InternalEntry", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(changedEntities[0]) as InternalEntityEntry).IsKeyUnknown;
            Console.WriteLine($"IsKeyUnknown: {IsKeyUnknown}");

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
