using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Customer.Get;

public class GetCustomerCommand
{
    public CustomerDto Customer { get; set; }
}

public class GetCustomerResult
{
    public GetCustomerResult(CustomerDto customer)
    {
        Customer = customer;
    }

    public CustomerDto Customer { get; set; }
}