using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities;

public record Dealer : IHasCreationTime, IHasModificationTime, IMustHaveTenant
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; set; }
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
    
    public Dealer(Guid id,string name, string location)
    {
        Id = id;
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
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}
