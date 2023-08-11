using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories.Interfaces;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.Application.Factories;

public class CustomerFactory : ICustomerFactory
{
    public CustomerDto ToDto(Customer customer)
    {
        return new CustomerDto(
            customer.Id,
            customer.FullName,
            customer.Address,
            customer.Phone,
            //cars,
            null,
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

        return customer;
    }
}