using Microsoft.Extensions.Logging;

using Tnf.CarShop.Application.Extensions;
using Tnf.CarShop.Application.Messages.Events;
using Tnf.CarShop.Domain.Repositories;

using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car.Create;

public class CreateCarCommandHandler : CommandHandler<CreateCarCommand, CreateCarResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ILogger<CreateCarCommandHandler> _logger;
    private readonly ICarEventPublisher _carEventPublisher;

    public CreateCarCommandHandler(
        ILogger<CreateCarCommandHandler> logger,
        ICarRepository carRepository,
        ICarEventPublisher carEventPublisher)
    {
        _logger = logger;
        _carRepository = carRepository;
        _carEventPublisher = carEventPublisher;
    }

    public override async Task<CreateCarResult> ExecuteAsync(CreateCarCommand command,
        CancellationToken cancellationToken = default)
    {
        var newCar = new Domain.Entities.Car(command.Brand, command.Model, command.Year, command.Price, command.StoreId);

        var createdCar = await _carRepository.InsertAsync(newCar, cancellationToken);

        if (createdCar is null || createdCar.Id == default)
        {
            _logger.EntityWasNotCreated("car");

            return new CreateCarResult(Guid.Empty, false);
        }

        _logger.EntitySuccessfullyCreated("car", createdCar.Id);

        await _carEventPublisher.NotifyCreationAsync(createdCar, cancellationToken);

        return new CreateCarResult(createdCar.Id, true);
    }    
}
