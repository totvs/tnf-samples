using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories.Interfaces;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.Application.Factories;

public class DealerFactory : IDealerFactory
{
    //private ICarFactory _carFactory;


    public DealerDto ToDto(Dealer dealer)
    {
        return new DealerDto(
            dealer.Id,
            dealer.Name,
            dealer.Location,
            null
        );
    }

    public Dealer ToEntity(DealerDto dealerDto)
    {
        var dealer = new Dealer(
            dealerDto.Id,
            dealerDto.Name,
            dealerDto.Location);

        return dealer;
    }

    public void SetCarFactory(ICarFactory carFactory)
    {
    }
}