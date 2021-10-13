using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleEntityFrameworkDemo.Data.Entities;
using System;

namespace SimpleEntityFrameworkDemo.Data.EntityConfigurations
{
    internal class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(b => new { b.Id, b.TenantId });
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(b => b.TenantId).HasValueGenerator<TenantIdGenerator>();

            builder.HasOne(b => b.Author).WithMany(a => a.Books)
                .HasPrincipalKey(b => new { b.Id, b.TenantId })
                .HasForeignKey(a => new { a.AuthorId, a.TenantId });
        }
    }
}
