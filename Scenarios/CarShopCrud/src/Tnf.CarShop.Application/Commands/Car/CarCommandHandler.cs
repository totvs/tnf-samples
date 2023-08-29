using Microsoft.Extensions.Logging;

using Tnf.CarShop.Application.Extensions;
using Tnf.CarShop.Application.Messages.Events;
using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Repositories;

using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car;
public class CarCommandHandler : CommandHandler<CarCommand, CarResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ILogger<CarCommandHandler> _logger;
    private readonly ICarEventPublisher _carEventPublisher;

    public CarCommandHandler(ICarRepository carRepository, ILogger<CarCommandHandler> logger, ICarEventPublisher carEventPublisher)
    {
        _carRepository = carRepository;
        _logger = logger;
        _carEventPublisher = carEventPublisher;
    }

    public override async Task<CarResult> ExecuteAsync(CarCommand command, CancellationToken cancellationToken = default)
    {
        Domain.Entities.Car car;

        if (!command.Id.HasValue)
        {
            car = await InsertCarAsync(command, cancellationToken);

            if (car is null || car.Id == default)
            {
                _logger.EntityWasNotCreated(nameof(car));
                return null;
            }

            _logger.EntitySuccessfullyCreated(nameof(car), car.Id);

            await _carEventPublisher.NotifyCreationAsync(car, cancellationToken);

            return new CarResult { CarDto = car.ToDto() };
        }

        car = await UpdateCarAsync(command, cancellationToken);

        _logger.EntitySuccessfullyUpdated(nameof(car), car.Id);

        var carDto = car.ToDto();
        await _carEventPublisher.NotifyUpdateAsync(carDto, cancellationToken);

        return new CarResult { CarDto = carDto };
    }

    private async Task<Domain.Entities.Car> UpdateCarAsync(CarCommand command, CancellationToken cancellationToken)
    {
        var car = await _carRepository.GetAsync(command.Id.Value, cancellationToken);

        if (car == null)
            throw new Exception($"Car with id {command.Id} not found.");

        car.UpdateBrand(command.Brand);
        car.UpdateModel(command.Model);
        car.UpdatePrice(command.Price);
        car.UpdateYear(command.Year);

        car = await _carRepository.UpdateAsync(car, cancellationToken);            

        return car;
    }

    private async Task<Domain.Entities.Car> InsertCarAsync(CarCommand command, CancellationToken cancellationToken)
    {
        var car = new Domain.Entities.Car(command.Brand, command.Model, command.Year, command.Price, command.StoreId);

        car = await _carRepository.InsertAsync(car, cancellationToken);               

        return car;
    }
}
