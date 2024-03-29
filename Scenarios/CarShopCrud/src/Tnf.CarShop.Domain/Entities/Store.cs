﻿using Tnf.CarShop.Domain.Dtos;

using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities;

public class Store : IHasCreationTime, IHasModificationTime, IMustHaveTenant
{
    private readonly List<Car> _cars = new();
    private readonly List<Customer> _customers = new();

    public Guid Id { get; set; }
    public string Name { get; private set; }
    public string Cnpj { get; private set; }
    public string Location { get; private set; }

    public IReadOnlyCollection<Car> Cars => _cars;
    public IReadOnlyCollection<Customer> Customers => _customers;
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }

    public Guid TenantId { get; set; }

    public Store(string name, string cnpj, string location)
    {
        Name = name;
        Cnpj = cnpj;
        Location = location;
    }


    public void UpdateLocation(string newLocation)
    {
        if (newLocation.IsNullOrEmpty())
            return;

        Location = newLocation;
    }

    public void UpdateCnpj(string cnpj)
    {
        if (cnpj.IsNullOrEmpty())
            return;

        Cnpj = cnpj;
    }

    public void AddCar(Car car)
    {
        _cars.Add(car);
    }

    public void UpdateName(string name)
    {
        if (name.IsNullOrEmpty())
            return;

        Name = name;
    }

    public void AddCustomer(Customer customer)
    {
        _customers.Add(customer);
    }

    public StoreDto ToDto()
    {
        return new StoreDto(Id, Name, Location, Cnpj);
    }
}
