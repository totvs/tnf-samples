public class GetPurchaseCommandHandler : ICommandHandler<GetPurchaseCommand, PurchaseResult>
{
    private readonly ILogger<GetPurchaseCommandHandler> _logger;
    private readonly IPurchaseRepository _purchaseRepository;

    public GetPurchaseCommandHandler(ILogger<GetPurchaseCommandHandler> logger, IPurchaseRepository purchaseRepository)
    {
        _logger = logger;
        _purchaseRepository = purchaseRepository;
    }

    public async Task HandleAsync(ICommandContext<GetPurchaseCommand, PurchaseResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var command = context.Command;

        var purchase = await _purchaseRepository.GetAsync(command.Id, cancellationToken);

        if (purchase == null)
        {
            throw new Exception($"Purchase with id {command.Id} not found.");
        }

        var purchaseResult = new PurchaseResult(purchase.Id, purchase.PurchaseDate, purchase.CustomerId, purchase.CarId, true);

        context.Result = purchaseResult;

        return;
    }
}