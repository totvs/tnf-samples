using Tnf.Repositories.Entities;

namespace Querying.Infra.Entities
{
    public class Product : Entity
    {
        public Product()
        {
        }

        public Product(string description, decimal value)
        {
            Description = description;
            Value = value;
        }

        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
