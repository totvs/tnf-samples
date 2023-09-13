using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Extensions;
using Tnf.CarShop.Application.Messages.Events;
using Tnf.CarShop.Domain.Repositories;

using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car;
public class CarCommandCreateHandler : CommandHandler<CarCommandCreate, CarResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ILogger<CarCommandCreateHandler> _logger;
    private readonly ICarEventPublisher _carEventPublisher;

    public CarCommandCreateHandler(ICarRepository carRepository, ILogger<CarCommandCreateHandler> logger, ICarEventPublisher carEventPublisher)
    {
        _carRepository = carRepository;
        _logger = logger;
        _carEventPublisher = carEventPublisher;
    }

    public override async Task<CarResult> ExecuteAsync(CarCommandCreate command, CancellationToken cancellationToken = default)
    {
        var car = new Domain.Entities.Car(command.Brand, command.Model, command.Year, command.Price, command.StoreId);

        car = await _carRepository.InsertAsync(car, cancellationToken);

        if (car is null || car.Id == Guid.Empty)
        {
            _logger.EntityWasNotCreated(nameof(car));
            return null;
        }

        _logger.EntitySuccessfullyCreated(nameof(car), car.Id);

        await _carEventPublisher.NotifyCreationAsync(car, cancellationToken);

        return new CarResult { CarDto = car.ToDto() };


    }

}
