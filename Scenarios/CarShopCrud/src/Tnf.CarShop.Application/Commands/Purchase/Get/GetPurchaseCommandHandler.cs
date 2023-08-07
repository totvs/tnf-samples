using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Commands.Purchase.Get;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

public class GetPurchaseCommandHandler : ICommandHandler<GetPurchaseCommand, GetPurchaseResult>
{
    private readonly ILogger<GetPurchaseCommandHandler> _logger;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly PurchaseFactory _purchaseFactory;


    public GetPurchaseCommandHandler(ILogger<GetPurchaseCommandHandler> logger, IPurchaseRepository purchaseRepository, PurchaseFactory purchaseFactory)
    {
        _logger = logger;
        _purchaseRepository = purchaseRepository;
        _purchaseFactory = purchaseFactory;
    }

    public async Task HandleAsync(ICommandContext<GetPurchaseCommand, GetPurchaseResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var purchaseId = context.Command.PurchaseId;

        var purchase = await _purchaseRepository.GetAsync(purchaseId, cancellationToken);

        if (purchase == null)
        {
            throw new Exception($"Purchase with id {purchaseId} not found.");
        }
        
        var purchaseResult = new GetPurchaseResult(_purchaseFactory.ToDto(purchase));

        context.Result = purchaseResult;

        return;
    }
}