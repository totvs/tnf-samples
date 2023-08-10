using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer.Delete;

public interface IDeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, DeleteCustomerResult>
{
}
