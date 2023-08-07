using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;
using Entity = Tnf.CarShop.Domain.Entities;
namespace CarShop.Commands.Car.Create;

public class CreateCarCommandHandler : ICommandHandler<CreateCarCommand, CarResult>
{
    private readonly ILogger<CreateCarCommandHandler> _logger;
    private readonly ICarRepository _carRepository;

    public CreateCarCommandHandler(ILogger<CreateCarCommandHandler> logger, ICarRepository carRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
    }

    public async Task HandleAsync(ICommandContext<CreateCarCommand, CarResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {

        var command = context.Command;

        var createdCarId = await CreateCarAsync(command, cancellationToken);

        context.Result = new CarResult(createdCarId, true);

        return;
    }

    private async Task<Guid> CreateCarAsync(CreateCarCommand command, CancellationToken cancellationToken)
    {
        var newCar = new Entity.Car(command.Brand, command.Model, command.Year, command.Price, command.DealerId, command.OwnerId);

        var createdCar = await _carRepository.InsertAsync(newCar, cancellationToken);

        return createdCar.Id;
    }
}