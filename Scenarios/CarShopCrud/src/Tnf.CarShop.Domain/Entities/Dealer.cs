using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities
{
    public class Dealer : IHasCreationTime, IHasModificationTime
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Cnpj { get; private set; }
        public string Location { get; private set; }
        public ICollection<Car>? Cars { get; private set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }

        protected Dealer()
        {
            Cars = new HashSet<Car>();
        }

        public Dealer(string name, string location)
        {     
            Name = name;
            Location = location;
        }

        public void UpdateLocation(string newLocation)
        {          
            Location = newLocation;
        }
        
        public void UpdateCnpj(string cnpj)
        {          
            Cnpj = cnpj;
        }

        public void AddCar(Car car)
        {          
            Cars.Add(car);
            car.AssignToDealer(this);
            LastModificationTime = DateTime.Now;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}
