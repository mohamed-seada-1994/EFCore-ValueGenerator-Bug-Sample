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

            builder.HasKey(b => new { b.Id , b.TenantId});
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.HasOne(b => b.Author).WithMany(a => a.Books)
                .HasPrincipalKey(a => new { a.Id , a.TenantId})
                .HasForeignKey(b => new { b.AuthorId, b.TenantId });
        }
    }
}
