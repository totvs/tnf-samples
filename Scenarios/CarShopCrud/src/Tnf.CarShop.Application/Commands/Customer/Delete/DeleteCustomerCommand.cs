namespace Tnf.CarShop.Application.Commands.Customer.Delete;

public class DeleteCustomerCommand
{
    public Guid CustomerId { get; set; }
}

public class DeleteCustomerResult
{
    public DeleteCustomerResult(bool success)
    {
        Success = success;
    }

    public bool Success { get; set; }
}