﻿using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Purchase.Get;

public class GetPurchaseCommand
{
    public Guid PurchaseId { get; set; }
}

public class GetPurchaseResult
{
    public GetPurchaseResult(PurchaseDto purchase)
    {
        Purchase = purchase;
    }

    public PurchaseDto Purchase { get; set; }
}