using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer.Update;

public class UpdateCustomerCommandHandler : CommandHandler<UpdateCustomerCommand, UpdateCustomerResult>
{
    private readonly CustomerFactory _customerFactory;
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<UpdateCustomerCommandHandler> _logger;


    public UpdateCustomerCommandHandler(ILogger<UpdateCustomerCommandHandler> logger,
        ICustomerRepository customerRepository, CustomerFactory customerFactory)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _customerFactory = customerFactory;
    }

    public override async Task<UpdateCustomerResult> ExecuteAsync(UpdateCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        var customerDto = command.Customer;

        var customer = await _customerRepository.GetAsync(customerDto.Id, cancellationToken);

        if (customer == null) throw new Exception($"Customer with id {customerDto.Id} not found.");

        customer.UpdateFullName(customerDto.FullName);
        customer.UpdateAddress(customerDto.Address);
        customer.UpdatePhone(customerDto.Phone);
        customer.UpdateEmail(customerDto.Email);
        customer.UpdateDateOfBirth(customerDto.DateOfBirth);

        var updatedCustomer = await _customerRepository.UpdateAsync(customer, cancellationToken);

        var updatedCustomerDto = _customerFactory.ToDto(updatedCustomer);

        return new UpdateCustomerResult(updatedCustomerDto);
    }
}