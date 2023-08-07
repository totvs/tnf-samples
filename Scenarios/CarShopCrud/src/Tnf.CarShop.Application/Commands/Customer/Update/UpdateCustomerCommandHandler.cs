using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Customer;
using Tnf.Commands;

public class UpdateCustomerCommandHandler : ICommandHandler<CustomerCommand, CustomerResult>
{
    private readonly ILogger<UpdateCustomerCommandHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(ILogger<UpdateCustomerCommandHandler> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task HandleAsync(ICommandContext<CustomerCommand, CustomerResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var command = context.Command;

        var customer = await _customerRepository.GetAsync(command.Id, cancellationToken);

        if (customer == null)
        {
            throw new Exception($"Customer with id {command.Id} not found.");
        }

        customer.UpdateFullName(command.FullName);
        customer.UpdateAddress(command.Address);
        customer.UpdatePhone(command.Phone);
        customer.UpdateEmail(command.Email);
        customer.UpdateDateOfBirth(command.DateOfBirthDay);

        var updatedCustomer = await _customerRepository.UpdateAsync(customer, cancellationToken);

        context.Result = new CustomerResult(updatedCustomer.Id, true);

        return;
    }
}