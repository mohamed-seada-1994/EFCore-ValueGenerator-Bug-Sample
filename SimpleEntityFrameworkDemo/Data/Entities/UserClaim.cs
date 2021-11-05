using System;

namespace SimpleEntityFrameworkDemo.Data.Entities
{
    public class UserClaim
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string Claim { get; set; }
        public string Value { get; set; }
        
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
