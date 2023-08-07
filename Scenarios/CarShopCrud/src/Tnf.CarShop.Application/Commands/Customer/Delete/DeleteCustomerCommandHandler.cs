using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Host.Commands.Customer.Delete;

public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, DeleteCustomerResult>
{
    private readonly ILogger<DeleteCustomerCommandHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerCommandHandler(ILogger<DeleteCustomerCommandHandler> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task HandleAsync(ICommandContext<DeleteCustomerCommand, DeleteCustomerResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var customerId = context.Command.CustomerId;

        var success = await _customerRepository.DeleteAsync(customerId, cancellationToken);

        context.Result = new(success);

        return;
    }
}
