namespace Tnf.CarShop.Host.Commands.Purchase.Delete;

public class DeletePurchaseCommandHandler : ICommandHandler<DeletePurchaseCommand, CommandResult>
{
    private readonly ILogger<DeletePurchaseCommandHandler> _logger;
    private readonly IPurchaseRepository _purchaseRepository;

    public DeletePurchaseCommandHandler(ILogger<DeletePurchaseCommandHandler> logger, IPurchaseRepository purchaseRepository)
    {
        _logger = logger;
        _purchaseRepository = purchaseRepository;
    }

    public async Task HandleAsync(ICommandContext<DeletePurchaseCommand, CommandResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var command = context.Command;

        var success = await _purchaseRepository.DeleteAsync(command.Id, cancellationToken);

        context.Result = new CommandResult(success);

        return;
    }
}
