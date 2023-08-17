using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;
using Tnf.CarShop.Application.Dtos;
using System;
using Tnf.CarShop.Application.Commands.Car.Create;
using Tnf.CarShop.Application.Commands.Car.Update;

namespace Tnf.CarShop.Application.Commands.Car;

public class CarCommandHandler : CommandHandler<CarCommand, CarResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ILogger<CarCommandHandler> _logger;

    public CarCommandHandler(
        ILogger<CarCommandHandler> logger,
        ICarRepository carRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
    }

    public override async Task<CarResult> ExecuteAsync(CarCommand command, CancellationToken cancellationToken = default)
    {
        if (command.Id.HasValue)
        {
            var car = await _carRepository.GetAsync(command.Id.Value, cancellationToken);
            if (car == null) throw new Exception($"Car with id {command.Id.Value} not found.");

            car.UpdateBrand(command.Brand);
            car.UpdateModel(command.Model);
            car.UpdatePrice(command.Price);
            car.UpdateYear(command.Year);

            var updatedCar = await _carRepository.UpdateAsync(car, cancellationToken);
            var updatedCarDto = new CarDto(updatedCar.Id, updatedCar.Brand, updatedCar.Model, updatedCar.Year, updatedCar.Price);

            return new CarResult(updatedCarDto);
        }
        else
        {
            var newCar = new Domain.Entities.Car(command.Brand, command.Model, command.Year, command.Price);
            var createdCar = await _carRepository.InsertAsync(newCar, cancellationToken);

            return new CarResult(createdCar.Id, true);
        }
       
    }
}