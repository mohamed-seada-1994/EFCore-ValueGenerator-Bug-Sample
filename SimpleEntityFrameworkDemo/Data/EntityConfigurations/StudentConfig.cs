using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleEntityFrameworkDemo.Data.Entities;
using System;

namespace SimpleEntityFrameworkDemo.Data.EntityConfigurations
{
    internal class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");

            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property<Guid>("TenantId").HasValueGenerator<TenantIdGenerator>();
            
            builder.HasKey("Id", "TenantId");
        }
    }
}
