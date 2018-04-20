namespace Querying.Infra.Entities
{
    public class Customer : IEntity
    {
        public Customer()
        {
        }

        public Customer(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
