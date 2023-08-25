using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Purchase.Get;

public class GetPurchaseCommandHandler : CommandHandler<GetPurchaseCommand, GetPurchaseResult>
{
    private readonly ILogger<GetPurchaseCommandHandler> _logger;
    private readonly IPurchaseRepository _purchaseRepository;


    public GetPurchaseCommandHandler(ILogger<GetPurchaseCommandHandler> logger, IPurchaseRepository purchaseRepository)
    {
        _logger = logger;
        _purchaseRepository = purchaseRepository;
    }

    public override async Task<GetPurchaseResult> ExecuteAsync(GetPurchaseCommand command, CancellationToken cancellationToken = default)
    {
        if (command.PurchaseId.HasValue)
        {
            var purchaseDto = await _purchaseRepository.GetPurchaseDtoAsync(command.PurchaseId.Value, cancellationToken);

            if (purchaseDto is null)
                return null;

            var purchaseResult = new GetPurchaseResult(purchaseDto);

            return purchaseResult;
        }

        var purchasesDto = await _purchaseRepository.GetAllAsync(command.RequestAllPurchases, cancellationToken);        

        return new GetPurchaseResult(purchasesDto);
    }
}
