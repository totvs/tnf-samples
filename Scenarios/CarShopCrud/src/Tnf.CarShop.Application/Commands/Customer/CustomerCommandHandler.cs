using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer;

public class CustomerCommandHandler : CommandHandler<CustomerCommand, CustomerResult>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public override async Task<CustomerResult> ExecuteAsync(CustomerCommand command, CancellationToken cancellationToken = default)
    {
        Domain.Entities.Customer customer;

        if (!command.Id.HasValue)
        {
            customer = await InsertCustomerAsync(command, cancellationToken);

            return new CustomerResult { CustomerDto = customer.ToDto() };
        }

        customer = await UpdateCustomerAsync(command, cancellationToken);

        return new CustomerResult { CustomerDto = customer.ToDto() };
    }

    private async Task<Domain.Entities.Customer> UpdateCustomerAsync(CustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsync(command.Id.Value, cancellationToken);

        if (customer == null)
            throw new Exception($"Customer with id {command.Id} not found.");

        customer.UpdateFullName(command.FullName);
        customer.UpdateAddress(command.Address);
        customer.UpdatePhone(command.Phone);
        customer.UpdateEmail(command.Email);
        customer.UpdateDateOfBirth(command.DateOfBirth);

        customer = await _customerRepository.UpdateAsync(customer, cancellationToken);

        return customer;
    }

    private async Task<Domain.Entities.Customer> InsertCustomerAsync(CustomerCommand command, CancellationToken cancellationToken)
    {
        var newCustomer = new Domain.Entities.Customer(command.FullName, command.Address, command.Phone, command.Email, command.DateOfBirth, command.StoreId);

        newCustomer = await _customerRepository.InsertAsync(newCustomer, cancellationToken);

        return newCustomer;
    }
}
