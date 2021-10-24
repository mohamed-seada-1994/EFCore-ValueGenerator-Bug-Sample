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

            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property<Guid>("TenantId").HasValueGenerator<TenantIdGenerator>();

            builder.HasKey("Id", "TenantId");
            builder.HasOne(b => b.Author).WithMany(a => a.Books)
                .HasPrincipalKey("Id","TenantId")
                .HasForeignKey("AuthorId", "TenantId");
        }
    }
}
