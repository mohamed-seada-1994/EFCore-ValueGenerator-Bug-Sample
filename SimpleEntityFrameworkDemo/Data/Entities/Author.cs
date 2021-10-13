using System;
using System.Collections.Generic;

namespace SimpleEntityFrameworkDemo.Data.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
