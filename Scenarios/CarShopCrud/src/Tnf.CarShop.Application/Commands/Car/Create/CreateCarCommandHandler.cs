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
        var createdCarId = await CreateCarAsync(command, cancellationToken);

        return new CreateCarResult(createdCarId, true);
    }

    private async Task<Guid> CreateCarAsync(CreateCarCommand command, CancellationToken cancellationToken)
    {
        var newCar = new Domain.Entities.Car(command.Brand, command.Model, command.Year, command.Price, command.StoreId);

        var createdCar = await _carRepository.InsertAsync(newCar, cancellationToken);

        _logger.EntitySuccessfullyCreated("car", createdCar.Id);

        await _carEventPublisher.NotifyCreationAsync(createdCar, cancellationToken);

        return createdCar.Id;
    }
}
