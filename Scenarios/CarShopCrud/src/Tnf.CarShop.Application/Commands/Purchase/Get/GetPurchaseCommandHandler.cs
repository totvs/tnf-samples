using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Purchase.Get;

public class GetPurchaseCommandHandler : CommandHandler<GetPurchaseCommand, GetPurchaseResult>
{
    private readonly ILogger<GetPurchaseCommandHandler> _logger;
    private readonly PurchaseFactory _purchaseFactory;
    private readonly IPurchaseRepository _purchaseRepository;


    public GetPurchaseCommandHandler(ILogger<GetPurchaseCommandHandler> logger, IPurchaseRepository purchaseRepository,
        PurchaseFactory purchaseFactory)
    {
        _logger = logger;
        _purchaseRepository = purchaseRepository;
        _purchaseFactory = purchaseFactory;
    }

    public override async Task<GetPurchaseResult> ExecuteAsync(GetPurchaseCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.PurchaseId.HasValue)
        {
            var purchase = await _purchaseRepository.GetAsync(command.PurchaseId.Value, cancellationToken);

            if (purchase == null) throw new Exception($"Purchase with id {command} not found.");

            var purchaseResult = new GetPurchaseResult(_purchaseFactory.ToDto(purchase));

            return purchaseResult;
        }

        var purchases = await _purchaseRepository.GetAllAsync(cancellationToken);

        var purchasesDto = purchases.Select(_purchaseFactory.ToDto).ToList();

        return new GetPurchaseResult(purchasesDto);
    }
}