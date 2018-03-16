using System.Collections.Generic;
using Tnf.Repositories.Entities;

namespace Querying.Infra.Entities
{
    public class Customer : Entity
    {
        public Customer()
        {
        }

        public Customer(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
