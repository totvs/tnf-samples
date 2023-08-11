using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories.Interfaces;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.Application.Factories;

public interface ICarFactory : IFactory<CarDto, Car>
{
}

public class CarFactory : ICarFactory
{
    public CarDto ToDto(Car car)
    {
        return new CarDto(
            car.Id,
            car.Brand,
            car.Model,
            car.Year,
            car.Price, null, null
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

        return car;
    }
}