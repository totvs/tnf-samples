using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer.Create;

public interface ICreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, CreateCustomerResult>
{
}
