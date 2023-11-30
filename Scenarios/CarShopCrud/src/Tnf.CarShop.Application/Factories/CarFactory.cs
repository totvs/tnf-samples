using Tnf.CarShop.Application.Commands.Car.Create;
using Tnf.CarShop.Application.Commands.Car.Update;
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
            car.Price
        );
    }

    public Car ToEntity(CreateCarCommand command)
    {
        var car = new Car(
            command.Brand,
            command.Model,
            command.Year,
            command.Price);

        return car;
    }

    public Car ToEntity(UpdateCarCommand command)
    {
        var car = new Car(
            command.Brand,
            command.Model,
            command.Year,
            command.Price);

        return car;
    }
}