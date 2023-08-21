﻿using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
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

    public override async Task<GetPurchaseResult> ExecuteAsync(GetPurchaseCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.PurchaseId.HasValue)
        {
            var purchase = await _purchaseRepository.GetAsync(command.PurchaseId.Value, cancellationToken);

            if (purchase == null) throw new Exception($"Purchase with id {command} not found.");

            var purchaseDto = new PurchaseDto(purchase.Id, purchase.PurchaseDate,
                purchase.CustomerId, purchase.CarId, purchase.TenantId);

            var purchaseResult = new GetPurchaseResult(purchaseDto);

            return purchaseResult;
        }

        var purchases = await _purchaseRepository.GetAllAsync(cancellationToken);

        var purchasesDto = purchases.Select(
            purchase => new PurchaseDto(purchase.Id, purchase.PurchaseDate, purchase.CustomerId,
                purchase.CarId, purchase.TenantId)
        ).ToList();

        return new GetPurchaseResult(purchasesDto);
    }
}