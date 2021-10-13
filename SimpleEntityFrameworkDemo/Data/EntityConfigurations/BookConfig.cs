using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleEntityFrameworkDemo.Data.Entities;

namespace SimpleEntityFrameworkDemo.Data.EntityConfigurations
{
    internal class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(b => new { b.TenantId, b.Id });
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.HasOne(b => b.Author).WithMany(a => a.Books)
                .HasPrincipalKey(a => new { a.TenantId, a.Id })
                .HasForeignKey(b => new { b.TenantId, b.Id });
        }
    }
}
