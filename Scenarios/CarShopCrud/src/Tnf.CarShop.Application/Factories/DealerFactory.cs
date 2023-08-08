using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories.Interfaces;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.Application.Factories;

public abstract class DealerFactory : IFactory<DealerDto, Dealer>
{
    private readonly CarFactory _carFactory;

    protected DealerFactory(CarFactory carFactory)
    {
        _carFactory = carFactory;
    }

    public DealerDto ToDto(Dealer dealer)
    {
        var cars = dealer.Cars?.Select(_carFactory.ToDto).ToList() ?? new List<CarDto>();
        return new DealerDto(
            dealer.Id,
            dealer.Name,
            dealer.Location,
            cars
        );
    }

    public Dealer ToEntity(DealerDto dealerDto)
    {
        var dealer = new Dealer(
            dealerDto.Id,
            dealerDto.Name,
            dealerDto.Location);


        if (dealerDto.Cars != null)
            foreach (var carDto in dealerDto.Cars)
            {
                var car = _carFactory.ToEntity(carDto);
                dealer.AddCar(car);
            }

        return dealer;
    }
}