using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;

using Tnf.Commands;

namespace Tnf.CarShop.Host.Commands.Car.Get;
public class GetCarCommandHandler : ICommandHandler<GetCarCommand, GetCarResult>
{
    private readonly ILogger<GetCarCommandHandler> _logger;
    private readonly ICarRepository _carRepository;

    public GetCarCommandHandler(ILogger<GetCarCommandHandler> logger, ICarRepository carRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
    }

    public async Task HandleAsync(ICommandContext<GetCarCommand, GetCarResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var command = context.Command;

        if (command.CarId.HasValue)
        {
            var car = await _carRepository.GetAsync(command.CarId.Value, cancellationToken);

            if (car == null)
            {
                throw new Exception($"Car with id {command.CarId} not found.");
            }

            var carResult = new GetCarResult( new CarDto(car.Id, car.Brand, car.Model, car.Year, car.Price, car.Dealer?.Id, car.Owner?.Id));
            context.Result = carResult;
        }

        return;
    }
}