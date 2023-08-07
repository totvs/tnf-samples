namespace Tnf.CarShop.Host.Commands.Customer.Delete;

public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, CommandResult>
{
    private readonly ILogger<DeleteCustomerCommandHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerCommandHandler(ILogger<DeleteCustomerCommandHandler> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task HandleAsync(ICommandContext<DeleteCustomerCommand, CommandResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var command = context.Command;

        var success = await _customerRepository.DeleteAsync(command.Id, cancellationToken);

        context.Result = new CommandResult(success);

        return;
    }
}
