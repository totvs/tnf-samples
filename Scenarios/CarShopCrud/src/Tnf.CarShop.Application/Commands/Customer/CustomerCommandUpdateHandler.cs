using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer;
public class CustomerCommandUpdateHandler : CommandHandler<CustomerCommandUpdate, CustomerResult>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerCommandUpdateHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public override async Task<CustomerResult> ExecuteAsync(CustomerCommandUpdate command, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetAsync(command.Id.Value, cancellationToken);

        if (customer is null)
        {
            throw new Exception($"Customer with id {command.Id} not found.");
        }

        customer.UpdateFullName(command.FullName);
        customer.UpdateAddress(command.Address);
        customer.UpdatePhone(command.Phone);
        customer.UpdateEmail(command.Email);
        customer.UpdateDateOfBirth(command.DateOfBirth);

        customer = await _customerRepository.UpdateAsync(customer, cancellationToken);

        return new CustomerResult { CustomerDto = customer.ToDto() };
    }
}
