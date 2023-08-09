using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories.Interfaces;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.Application.Factories;

public abstract record CarFactory : IFactory<CarDto, Car>
{
    private readonly CustomerFactory _customerFactory;
    private readonly DealerFactory _dealerFactory;

    protected CarFactory(DealerFactory dealerFactory, CustomerFactory customerFactory)
    {
        _dealerFactory = dealerFactory;
        _customerFactory = customerFactory;
    }

    public CarDto ToDto(Car car)
    {
        return new CarDto(
            car.Id,
            car.Brand,
            car.Model,
            car.Year,
            car.Price,
            car.Dealer != null ? _dealerFactory.ToDto(car.Dealer) : null,
            car.Owner != null ? _customerFactory.ToDto(car.Owner) : null
        );
    }

    public Car ToEntity(CarDto carDto)
    {
        var car = new Car(
            carDto.Id,
            carDto.Brand,
            carDto.Model,
            carDto.Year,
            carDto.Price);

        if (carDto.Dealer != null) car.AssignToDealer(_dealerFactory.ToEntity(carDto.Dealer));

        if (carDto.Owner != null) car.AssignToOwner(_customerFactory.ToEntity(carDto.Owner));

        return car;
    }
}