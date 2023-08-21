using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car.Create;

public class CreateCarCommandHandler : CommandHandler<CreateCarCommand, CreateCarResult>
{
    private readonly ICarRepository _carRepository;
    private readonly IStoreRepository _storeRepository;
    private readonly ILogger<CreateCarCommandHandler> _logger;


    public CreateCarCommandHandler(ILogger<CreateCarCommandHandler> logger, ICarRepository carRepository,
        IStoreRepository storeRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
        _storeRepository = storeRepository;
    }

    public override async Task<CreateCarResult> ExecuteAsync(CreateCarCommand command,
        CancellationToken cancellationToken = default)
    {
        var createdCarId = await CreateCarAsync(command, cancellationToken);

        return new CreateCarResult(createdCarId, true);
    }

    private async Task<Guid> CreateCarAsync(CreateCarCommand command, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetAsync(command.TenantId, cancellationToken);

        var newCar = new Domain.Entities.Car(command.Brand, command.Model, command.Year, command.Price, store, command.TenantId);

        var createdCar = await _carRepository.InsertAsync(newCar, cancellationToken);

        return createdCar.Id;
    }
}

