namespace Dapper.Infra.Entities
{
    public class Product : IEntity
    {
        public Product()
        {
        }

        public Product(string description, decimal value)
        {
            Description = description;
            Value = value;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
