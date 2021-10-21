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

            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property<Guid>("TenantId").HasValueGenerator<TenantIdGenerator>();
            
            builder.HasKey("Id", "TenantId");
        }
    }
}
