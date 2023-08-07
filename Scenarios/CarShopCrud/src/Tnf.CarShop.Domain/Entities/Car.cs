using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities
{
    public class Car : IHasCreationTime, IHasModificationTime
    {
        public Guid Id { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public decimal Price { get; private set; }
        public Dealer? Dealer { get; private set; }
        public Customer? Owner { get; private set; }
        private decimal Discount { get; set; }
        public bool IsNew { get { return DateTime.Now.Year - Year <= 1; } }
        public bool IsOld { get { return DateTime.Now.Year - Year > 20; } }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }

        protected Car(Customer owner, string brand, string model, Dealer dealer)
        {
            Owner = owner;
            Brand = brand;
            Model = model;
            Dealer = dealer;
        }
  
        public Car(string brand, string model, int year, decimal price)
        {            
            Brand = brand;
            Model = model;
            Year = year;
            Price = price;
       
            CreationTime = DateTime.Now;
        }
        public void ApplyDiscount(decimal percentage)
        {
            Discount = (Price * percentage) / 100;
            Price -= Discount;

            LastModificationTime = DateTime.Now;
        }

        public decimal GetDiscountedPrice()
        {
            return Price - Discount;
        }

        public void UpdatePrice(decimal newPrice)
        {      
            Price = newPrice;
            LastModificationTime = DateTime.Now;
        }

        public void AssignToDealer(Dealer newDealer)
        {
            Dealer = newDealer;
            LastModificationTime = DateTime.Now;
        }

        public void UpdateBrand(string brand)
        {
            Brand = brand;
            LastModificationTime = DateTime.Now;
        }

        public void UpdateModel(string model)
        {
            Model = model;
            LastModificationTime = DateTime.Now;
        }

        public void UpdateYear(int year)
        {
            Year = year;
            LastModificationTime = DateTime.Now;
        }

        public void AssignToOwner(Customer owner)
        {
            Owner = owner;
            LastModificationTime = DateTime.Now;
        }
    }
}
