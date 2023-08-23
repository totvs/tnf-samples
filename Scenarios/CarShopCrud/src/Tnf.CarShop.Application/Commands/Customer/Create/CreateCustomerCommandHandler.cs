using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer.Create;

public class CreateCustomerCommandHandler : CommandHandler<CreateCustomerCommand, CreateCustomerResult>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public override async Task<CreateCustomerResult> ExecuteAsync(CreateCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        var createdCustomerId = await CreateCustomerAsync(command, cancellationToken);

        return new CreateCustomerResult(createdCustomerId, true);
    }

    private async Task<Guid> CreateCustomerAsync(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var newCustomer = new Domain.Entities.Customer(command.FullName, command.Address, command.Phone, command.Email, command.DateOfBirth, command.StoreId);

        var createdCustomer = await _customerRepository.InsertAsync(newCustomer, cancellationToken);

        return createdCustomer.Id;
    }
}
