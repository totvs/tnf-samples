using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer.Update;
 xunit
public class UpdateCustomerCommandHandler : CommandHandler<UpdateCustomerCommand, UpdateCustomerResult>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<UpdateCustomerCommandHandler> _logger;


    public UpdateCustomerCommandHandler(ILogger<UpdateCustomerCommandHandler> logger,
        ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public override async Task<UpdateCustomerResult> ExecuteAsync(UpdateCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetAsync(command.Id, cancellationToken);

        if (customer == null) throw new Exception($"Customer with id {command.Id} not found.");

        customer.UpdateFullName(command.FullName);
        customer.UpdateAddress(command.Address);
        customer.UpdatePhone(command.Phone);
        customer.UpdateEmail(command.Email);
        customer.UpdateDateOfBirth(command.DateOfBirth);

        var updatedCustomer = await _customerRepository.UpdateAsync(customer, cancellationToken);

        var updatedCustomerDto = new CustomerDto(updatedCustomer.Id, updatedCustomer.FullName, updatedCustomer.Address,
            updatedCustomer.Phone, updatedCustomer.Email, updatedCustomer.DateOfBirth);

        return new UpdateCustomerResult(updatedCustomerDto);
    }
}
//Unit Test
