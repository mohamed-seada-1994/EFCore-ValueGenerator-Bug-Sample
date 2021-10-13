using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace SimpleEntityFrameworkDemo.Data
{
    public class TenantIdGenerator : ValueGenerator<string>
    {
        public override string Next(EntityEntry entry)
            => Guid.Parse("98D06A82-C691-4988-EA39-08D98E2C8D8F").ToString();

        public override bool GeneratesTemporaryValues => false;
    }
}
