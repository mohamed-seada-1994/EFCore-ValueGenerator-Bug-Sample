using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleEntityFrameworkDemo.Data.Entities;
using System;

namespace SimpleEntityFrameworkDemo.Data.EntityConfigurations
{
    internal class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");

            builder.HasKey(x => new { x.Id, x.TenantId });
            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TenantId).HasValueGenerator<TenantIdGenerator>();
        }
    }
}
