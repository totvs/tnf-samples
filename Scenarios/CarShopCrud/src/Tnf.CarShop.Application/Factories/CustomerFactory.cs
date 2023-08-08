using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories.Interfaces;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.Application.Factories;

public abstract class CustomerFactory : IFactory<CustomerDto, Customer>
{
    private readonly CarFactory _carFactory;

    protected CustomerFactory(CarFactory carFactory)
    {
        _carFactory = carFactory;
    }

    public CustomerDto ToDto(Customer customer)
    {
        var cars = customer.CarsOwned.Select(_carFactory.ToDto).ToList();
        return new CustomerDto(
            customer.Id,
            customer.FullName,
            customer.Address,
            customer.Phone,
            cars,
            customer.Email,
            customer.DateOfBirth
        );
    }

    public Customer ToEntity(CustomerDto customerDto)
    {
        var customer = new Customer(
            customerDto.Id,
            customerDto.FullName,
            customerDto.Address,
            customerDto.Phone,
            customerDto.Email,
            customerDto.DateOfBirth);


        if (customerDto.Cars != null)
            foreach (var carDto in customerDto.Cars)
            {
                var car = _carFactory.ToEntity(carDto);
                customer.PurchaseCar(car);
            }

        return customer;
    }
}