using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Customer.Update;

public class UpdateCustomerCommand
{
    public CustomerDto Customer { get; set; }
}

public class UpdateCustomerResult
{
    public UpdateCustomerResult(CustomerDto customer)
    {
        Customer = customer;
    }

    public CustomerDto Customer { get; set; }
}