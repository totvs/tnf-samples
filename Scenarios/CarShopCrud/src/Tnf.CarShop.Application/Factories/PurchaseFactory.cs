using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories.Interfaces;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.Application.Factories;

public class PurchaseFactory : IPurchaseFactory
{
    public PurchaseDto ToDto(Purchase purchase)
    {
        return new PurchaseDto(
            purchase.Id,
            purchase.PurchaseDate
        );
    }

    public Purchase ToEntity(PurchaseDto purchaseDto)
    {
        var purchase = new Purchase(purchaseDto.Id, purchaseDto.PurchaseDate);

        return purchase;
    }
}