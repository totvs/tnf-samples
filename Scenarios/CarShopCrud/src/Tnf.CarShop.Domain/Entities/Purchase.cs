using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities
{
    public class Purchase : IHasCreationTime
    {
        public Guid Id { get; private set; }
        public DateTime PurchaseDate { get; private set; }
        public Customer Customer { get; private set; }
        public Car Car { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreationTime { get; set; }

        protected Purchase() { }

        public Purchase(Customer customer, Car car)
        {
            Customer = customer;
            Car = car;
            Price = car.Price;
            PurchaseDate = DateTime.Now;
            CreationTime = DateTime.Now;
        }

        public void CompletePurchase()
        {
            Car.AssignToOwner(Customer);
        }
    }
}
