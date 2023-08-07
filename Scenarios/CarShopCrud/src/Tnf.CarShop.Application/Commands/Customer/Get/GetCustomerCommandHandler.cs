using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Customer;
using Tnf.Commands;

public class GetCustomerCommandHandler : ICommandHandler<CustomerCommand, CustomerResult>
{
    private readonly ILogger<GetCustomerCommandHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerCommandHandler(ILogger<GetCustomerCommandHandler> logger, ICustomerRepository customerRepository)
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

        var customerResult = new CustomerResult(customer.Id, customer.FullName, customer.Address, customer.Phone, true);

        context.Result = customerResult;

        return;
    }
}