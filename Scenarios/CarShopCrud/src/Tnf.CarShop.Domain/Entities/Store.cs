﻿using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities;

public class Store : IHasCreationTime, IHasModificationTime, IMustHaveTenant
{
    private readonly List<Car> _cars = new List<Car>();

    public Guid TenantId { get; set; }
    public string Name { get; private set; }
    public string Cnpj { get; private set; }
    public string Location { get; private set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
        
    public IReadOnlyCollection<Car> Cars => _cars;

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
}