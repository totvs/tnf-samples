using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities
{
    public class Customer : IHasCreationTime, IHasModificationTime
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public  ICollection<Car> CarsOwned { get; private set; }
        
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }

        protected Customer()
        {
            CarsOwned = new HashSet<Car>();
        }

        public Customer(string fullName, string address, string phone, string email, DateOnly dateOfBirth)
        {
            FullName = fullName;
            Address = address;
            Phone = phone;
            Email = email;
            DateOfBirth = dateOfBirth;
            CreationTime = DateTime.Now;
        }

 
        public void UpdateFullName(string fullName)
        {
            FullName = fullName;
            LastModificationTime = DateTime.Now;
        }

        public void UpdateAddress(string address)
        {
            Address = address;
            LastModificationTime = DateTime.Now;
        }

        public void UpdatePhone(string phone)
        {
            Phone = phone;
            LastModificationTime = DateTime.Now;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
            LastModificationTime = DateTime.Now;
        }

        public void UpdateDateOfBirth(DateOnly dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
            LastModificationTime = DateTime.Now;
        }

        public void PurchaseCar(Car car)
        {
            CarsOwned.Add(car);
            LastModificationTime = DateTime.Now;
        }   
    }
}
