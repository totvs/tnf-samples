using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer.Create;

public class CreateCustomerCommandHandler : CommandHandler<CreateCustomerCommand, CreateCustomerResult>
{
    private readonly CustomerFactory _customerFactory;
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, CustomerFactory customerFactory)
    {
        _customerRepository = customerRepository;
        _customerFactory = customerFactory;
    }

    public override async Task<CreateCustomerResult> ExecuteAsync(CreateCustomerCommand command, CancellationToken cancellationToken = default)
    {
        var customerDto = command.Customer;

        var createdCustomerId = await CreateCustomerAsync(customerDto, cancellationToken);

        return new CreateCustomerResult(createdCustomerId, true);
    }    

    private async Task<Guid> CreateCustomerAsync(CustomerDto customerDto, CancellationToken cancellationToken)
    {
        var newCustomer = _customerFactory.ToEntity(customerDto);

        var createdCustomer = await _customerRepository.InsertAsync(newCustomer, cancellationToken);

        return createdCustomer.Id;
    }
}