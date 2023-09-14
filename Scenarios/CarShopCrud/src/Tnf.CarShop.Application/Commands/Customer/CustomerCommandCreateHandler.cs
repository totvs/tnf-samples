using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer;

public class CustomerCommandCreateHandler : CommandHandler<CustomerCommandCreateAdmin, CustomerResult>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerCommandCreateHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public override async Task<CustomerResult> ExecuteAsync(CustomerCommandCreateAdmin command, CancellationToken cancellationToken = default)
    {
        var newCustomer = new Domain.Entities.Customer(command.FullName, command.Address, command.Phone, command.Email, command.DateOfBirth, command.StoreId);

        newCustomer = await _customerRepository.InsertAsync(newCustomer, cancellationToken);

        return new CustomerResult { CustomerDto = newCustomer.ToDto() };

    }
 
}
