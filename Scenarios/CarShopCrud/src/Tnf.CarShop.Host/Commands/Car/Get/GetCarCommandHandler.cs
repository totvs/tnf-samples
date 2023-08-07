using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Car.Create;
using Tnf.Commands;

public class GetCarCommandHandler : ICommandHandler<CarCommand, CarResult>
{
    private readonly ILogger<GetCarCommandHandler> _logger;
    private readonly ICarRepository _carRepository;

    public GetCarCommandHandler(ILogger<GetCarCommandHandler> logger, ICarRepository carRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
    }

    public async Task HandleAsync(ICommandContext<CarCommand, CarResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var command = context.Command;

        var car = await _carRepository.GetAsync(command.Id, cancellationToken);

        if (car == null)
        {
            throw new Exception($"Car with id {command.Id} not found.");
        }

        var carResult = new CarResult(car.Id, car.Brand, car.Model, car.Year, car.Price, true);

        context.Result = carResult;

        return;
    }
}