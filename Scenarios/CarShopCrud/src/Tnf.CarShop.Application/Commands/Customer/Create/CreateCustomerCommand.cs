using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Customer.Create;

public class CreateCustomerCommand
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public DateOnly DateOfBirth { get; set; }
}

public class CreateCustomerResult
{
    public CreateCustomerResult(Guid createdCustomerId, bool success)
    {
        CustomerId = createdCustomerId;
        Success = success;
    }

    public Guid CustomerId { get; set; }
    public bool Success { get; set; }
}