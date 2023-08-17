using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities;

public class Store : IHasCreationTime, IHasModificationTime, IMustHaveTenant
{
    private readonly List<Car> _cars = new();
    private readonly List<Customer> _customers = new();

    public Store(string name, string cnpj, string location)
    {
        Name = name;
        Cnpj = cnpj;
        Location = location;
    }

    public Store(Guid tenantId, string name, string cnpj, string location)
    {
        Name = name;
        Cnpj = cnpj;
        Location = location;
        TenantId = tenantId;
    }

    public string Name { get; private set; }
    public string Cnpj { get; private set; }
    public string Location { get; private set; }

    public IReadOnlyCollection<Car> Cars => _cars;
    public IReadOnlyCollection<Customer> Customers => _customers;
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }

    public Guid TenantId { get; set; }

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
        _cars.Add(car);
    }

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void AddCustomer(Customer customer)
    {
        _customers.Add(customer);
    }
}