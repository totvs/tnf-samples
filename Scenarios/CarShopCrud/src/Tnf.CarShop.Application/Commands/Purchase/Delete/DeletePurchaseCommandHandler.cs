using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Purchase.Delete;

public class DeletePurchaseCommandHandler : CommandHandler<DeletePurchaseCommand, DeletePurchaseResult>
{
    private readonly ILogger<DeletePurchaseCommandHandler> _logger;
    private readonly IPurchaseRepository _purchaseRepository;

    public DeletePurchaseCommandHandler(ILogger<DeletePurchaseCommandHandler> logger,
        IPurchaseRepository purchaseRepository)
    {
        _logger = logger;
        _purchaseRepository = purchaseRepository;
    }

    public override async Task<DeletePurchaseResult> ExecuteAsync(DeletePurchaseCommand command, CancellationToken cancellationToken = default)
    {
        var purchaseId = command.PurchaseId;

        await _purchaseRepository.DeleteAsync(purchaseId, cancellationToken);

        return new DeletePurchaseResult(true);
    }    
}