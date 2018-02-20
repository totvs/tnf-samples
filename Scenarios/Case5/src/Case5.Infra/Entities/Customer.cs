using System;
using Tnf.Repositories.Entities;

namespace Case5.Infra.Entities
{
    public class Customer : Entity<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
