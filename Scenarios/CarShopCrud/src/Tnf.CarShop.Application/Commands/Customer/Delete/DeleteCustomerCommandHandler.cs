using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer.Delete;

public class DeleteCustomerCommandHandler : CommandHandler<DeleteCustomerCommand, DeleteCustomerResult>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<DeleteCustomerCommandHandler> _logger;

    public DeleteCustomerCommandHandler(ILogger<DeleteCustomerCommandHandler> logger,
        ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public override async Task<DeleteCustomerResult> ExecuteAsync(DeleteCustomerCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var customerId = command.CustomerId;

            await _customerRepository.DeleteAsync(customerId, cancellationToken);

            return new DeleteCustomerResult(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new DeleteCustomerResult(false);
        }        
    }
}
