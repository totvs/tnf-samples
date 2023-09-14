using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Extensions;
using Tnf.CarShop.Application.Messages.Events;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car;
public class CarCommandUpdateHandler : CommandHandler<CarCommandUpdateAdmin, CarResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ILogger<CarCommandUpdateHandler> _logger;
    private readonly ICarEventPublisher _carEventPublisher;

    public CarCommandUpdateHandler(ICarRepository carRepository, ILogger<CarCommandUpdateHandler> logger, ICarEventPublisher carEventPublisher)
    {
        _carRepository = carRepository;
        _logger = logger;
        _carEventPublisher = carEventPublisher;
    }

    public override async Task<CarResult> ExecuteAsync(CarCommandUpdateAdmin command, CancellationToken cancellationToken = default)
    {
        var car = await _carRepository.GetAsync(command.Id.Value, cancellationToken);

        if (car == null)
            throw new Exception($"Car with id {command.Id} not found.");

        car.UpdateBrand(command.Brand);
        car.UpdateModel(command.Model);
        car.UpdatePrice(command.Price);
        car.UpdateYear(command.Year);

        car = await _carRepository.UpdateAsync(car, cancellationToken);

        _logger.EntitySuccessfullyUpdated(nameof(car), car.Id);

        await _carEventPublisher.NotifyUpdateAsync(car.ToDto(), cancellationToken);

        return new CarResult { CarDto = car.ToDto() };
    }
}
