using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Purchase.Delete;

public class DeletePurchaseCommandHandler : ICommandHandler<DeletePurchaseCommand, DeletePurchaseResult>
{
    private readonly ILogger<DeletePurchaseCommandHandler> _logger;
    private readonly IPurchaseRepository _purchaseRepository;

    public DeletePurchaseCommandHandler(ILogger<DeletePurchaseCommandHandler> logger,
        IPurchaseRepository purchaseRepository)
    {
        _logger = logger;
        _purchaseRepository = purchaseRepository;
    }

    public async Task HandleAsync(ICommandContext<DeletePurchaseCommand, DeletePurchaseResult> context,
        CancellationToken cancellationToken = new())
    {
        var purchaseId = context.Command.PurchaseId;

        await _purchaseRepository.DeleteAsync(purchaseId, cancellationToken);

        context.Result = new DeletePurchaseResult(true);
    }
}