using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer.Get;

public class GetCustomerCommandHandler : CommandHandler<GetCustomerCommand, GetCustomerResult>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public override async Task<GetCustomerResult> ExecuteAsync(GetCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.CustomerId.HasValue)
        {
            var customer = await _customerRepository.GetAsync(command.CustomerId.Value, cancellationToken);

            if (customer == null) throw new Exception($"Customer with id {command.CustomerId.Value} not found.");

            var customerDto = new CustomerDto(customer.Id, customer.FullName, customer.Address, customer.Phone,
                customer.Email, customer.DateOfBirth);

            return new GetCustomerResult(customerDto);
        }

        var customersDto = await _customerRepository.GetAllAsync(command.RequestAllCustomers, cancellationToken);
        

        return new GetCustomerResult(customersDto);
    }
}
