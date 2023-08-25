using Microsoft.Extensions.Logging;

using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Application.Messages.Events;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Application.Extensions;

using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car.Update;

public class UpdateCarCommandHandler : CommandHandler<UpdateCarCommand, UpdateCarResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ILogger<UpdateCarCommandHandler> _logger;
    private readonly ICarEventPublisher _carEventPublisher;

    public UpdateCarCommandHandler(ILogger<UpdateCarCommandHandler> logger, ICarRepository carRepository, ICarEventPublisher carEventPublisher)
    {
        _logger = logger;
        _carRepository = carRepository;
        _carEventPublisher = carEventPublisher;
    }

    public override async Task<UpdateCarResult> ExecuteAsync(UpdateCarCommand command, CancellationToken cancellationToken = default)
    {
        var car = await _carRepository.GetAsync(command.Id, cancellationToken);

        if (car == null) throw new Exception($"Car with id {command.Id} not found.");

        car.UpdateBrand(command.Brand);
        car.UpdateModel(command.Model);
        car.UpdatePrice(command.Price);
        car.UpdateYear(command.Year);

        var updatedCar = await _carRepository.UpdateAsync(car, cancellationToken);

        var updatedCarDto = new CarDto(
            updatedCar.Id,
            updatedCar.Brand,
            updatedCar.Model,
            updatedCar.Year,
            updatedCar.Price,
            updatedCar.StoreId);

        _logger.EntitySuccessfullyUpdated(nameof(car), updatedCar.Id);

        await _carEventPublisher.NotifyUpdateAsync(updatedCarDto, cancellationToken);

        return new UpdateCarResult(updatedCarDto);
    }
}
