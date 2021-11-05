using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleEntityFrameworkDemo.Data.Entities;
using System;

namespace SimpleEntityFrameworkDemo.Data.EntityConfigurations
{
    internal class UserClaimConfig : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable("UserClaims");

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property<Guid>("TenantId").HasValueGenerator<TenantIdGenerator>();

            builder.HasKey("Id", "TenantId");
            builder.HasOne(c => c.User).WithMany(u => u.Claims)
                .HasPrincipalKey("Id")
                .HasForeignKey("UserId");
        }
    }
}
