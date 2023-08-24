using Tnf.CarShop.Domain.Dtos;
using Tnf.Dto;

namespace Tnf.CarShop.Application.Commands.Customer.Get;

public class GetCustomerCommand
{
    public Guid? CustomerId { get; set; }
    public RequestAllDto RequestAllCustomers { get; set; }
}

public class GetCustomerResult
{
    public GetCustomerResult(CustomerDto customer)
    {
        Customer = customer;
    }

    public GetCustomerResult(IListDto<CustomerDto> customers)
    {
        Customers = customers;
    }

    public IListDto<CustomerDto> Customers { get; set; }
    public CustomerDto Customer { get; set; }
}
