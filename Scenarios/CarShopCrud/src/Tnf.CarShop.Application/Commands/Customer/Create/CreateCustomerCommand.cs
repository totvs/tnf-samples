using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Host.Commands.Customer
{
    public class CreateCustomerCommand
    {
        public CustomerDto Customer { get; set; }
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
}