using System;

namespace SimpleEntityFrameworkDemo.Data.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}
