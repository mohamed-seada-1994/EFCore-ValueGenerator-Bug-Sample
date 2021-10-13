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

            builder.Property(x => x.TenantId).HasValueGenerator<TenantIdGenerator>();
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.HasKey(x => new { x.Id, x.TenantId });

            builder.HasOne(b => b.Author).WithMany(a => a.Books)
                .HasPrincipalKey(x => new { x.Id, x.TenantId })
                .HasForeignKey(x => new { x.AuthorId, x.TenantId });
        }
    }
}
