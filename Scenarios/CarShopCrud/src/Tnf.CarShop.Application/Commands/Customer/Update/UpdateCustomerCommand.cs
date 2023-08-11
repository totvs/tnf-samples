using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Customer.Update;

public class UpdateCustomerCommand
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public DateOnly DateOfBirth { get; set; }
}

public class UpdateCustomerResult
{
    public UpdateCustomerResult(CustomerDto customer)
    {
        Customer = customer;
    }

    public CustomerDto Customer { get; set; }
}