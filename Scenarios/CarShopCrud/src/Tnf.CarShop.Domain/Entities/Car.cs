using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities
{
    public class Car : IHasCreationTime, IHasModificationTime, IMayHaveTenant
    {
        public Guid Id { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public decimal Price { get; private set; }
        private decimal Discount { get; set; }
        public bool IsNew { get { return DateTime.Now.Year - Year <= 1; } }
        public bool IsOld { get { return DateTime.Now.Year - Year > 20; } }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Dealer Dealer { get; private set; }
        public Customer Owner { get; private set; }

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
            Dealer = dealer;
            Owner = owner;
            CreationTime = DateTime.Now;
            CreationTime = DateTime.Now;
        }
        
        public Car(Guid id, string brand, string model, int year, decimal price)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Year = year;
            Price = price;
        }
        public void ApplyDiscount(decimal percentage)
        {
            Discount = (Price * percentage) / 100;
            Price -= Discount;
        }

        public decimal GetDiscountedPrice()
        {
            return Price - Discount;
        }

        public void UpdatePrice(decimal newPrice)
        {      
            Price = newPrice;
        }

        public void AssignToDealer(Dealer newDealer)
        {
            Dealer = newDealer;
        }

        public void UpdateBrand(string brand)
        {
            Brand = brand;
        }

        public void UpdateModel(string model)
        {
            Model = model;
        }

        public void UpdateYear(int year)
        {
            Year = year;
        }

        public void AssignToOwner(Customer owner)
        {
            Owner = owner;
        }
    }
}
