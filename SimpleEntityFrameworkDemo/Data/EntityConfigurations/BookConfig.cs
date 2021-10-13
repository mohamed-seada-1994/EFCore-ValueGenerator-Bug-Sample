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
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.HasOne(b => b.Author).WithMany(a => a.Books)
                .HasPrincipalKey(a => a.Id)
                .HasForeignKey(b => b.AuthorId);
        }
    }
}
