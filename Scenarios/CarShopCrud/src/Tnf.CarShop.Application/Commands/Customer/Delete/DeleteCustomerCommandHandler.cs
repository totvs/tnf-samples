using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer.Delete;

public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, DeleteCustomerResult>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<DeleteCustomerCommandHandler> _logger;

    public DeleteCustomerCommandHandler(ILogger<DeleteCustomerCommandHandler> logger,
        ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task HandleAsync(ICommandContext<DeleteCustomerCommand, DeleteCustomerResult> context,
        CancellationToken cancellationToken = new())
    {
        var customerId = context.Command.CustomerId;

        await _customerRepository.DeleteAsync(customerId, cancellationToken);

        context.Result = new DeleteCustomerResult(true);
    }
}