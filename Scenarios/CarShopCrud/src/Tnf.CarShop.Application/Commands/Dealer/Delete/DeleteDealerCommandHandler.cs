namespace Tnf.CarShop.Host.Commands.Dealer.Delete;

public class DeleteDealerCommandHandler : ICommandHandler<DeleteDealerCommand, CommandResult>
{
    private readonly ILogger<DeleteDealerCommandHandler> _logger;
    private readonly IDealerRepository _dealerRepository;

    public DeleteDealerCommandHandler(ILogger<DeleteDealerCommandHandler> logger, IDealerRepository dealerRepository)
    {
        _logger = logger;
        _dealerRepository = dealerRepository;
    }

    public async Task HandleAsync(ICommandContext<DeleteDealerCommand, CommandResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var command = context.Command;

        var success = await _dealerRepository.DeleteAsync(command.Id, cancellationToken);

        context.Result = new CommandResult(success);

        return;
    }
}
