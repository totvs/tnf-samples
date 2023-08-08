using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories.Interfaces;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.Application.Factories;

public abstract class PurchaseFactory : IFactory<PurchaseDto, Purchase>
{
    private readonly CarFactory _carFactory;
    private readonly CustomerFactory _customerFactory;

    protected PurchaseFactory(CustomerFactory customerFactory, CarFactory carFactory)
    {
        _customerFactory = customerFactory;
        _carFactory = carFactory;
    }

    public PurchaseDto ToDto(Purchase purchase)
    {
        return new PurchaseDto(
            purchase.Id,
            purchase.PurchaseDate,
            _customerFactory.ToDto(purchase.Customer),
            _carFactory.ToDto(purchase.Car)
        );
    }

    public Purchase ToEntity(PurchaseDto purchaseDto)
    {
        var customer = _customerFactory.ToEntity(purchaseDto.Customer);
        var car = _carFactory.ToEntity(purchaseDto.Car);

        var purchase = new Purchase(purchaseDto.Id, customer, car, purchaseDto.PurchaseDate);

        return purchase;
    }
}