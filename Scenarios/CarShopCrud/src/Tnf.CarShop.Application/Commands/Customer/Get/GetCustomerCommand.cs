using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Customer.Get;

public class GetCustomerCommand
{
    public Guid? CustomerId { get; set; }
}

public class GetCustomerResult
{
    public GetCustomerResult(CustomerDto customer)
    {
        Customer = customer;
    }

    public GetCustomerResult(List<CustomerDto> customers)
    {
        Customers = customers;
    }

    public List<CustomerDto> Customers { get; set; }
    public CustomerDto Customer { get; set; }
}