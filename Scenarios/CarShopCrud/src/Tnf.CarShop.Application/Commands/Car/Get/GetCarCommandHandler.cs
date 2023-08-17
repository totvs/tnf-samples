using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car.Get;

public class GetCarCommandHandler : CommandHandler<GetCarCommand, GetCarResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ILogger<GetCarCommandHandler> _logger;

    public GetCarCommandHandler(ILogger<GetCarCommandHandler> logger, ICarRepository carRepository)
    {
        _logger = logger;
        _carRepository = carRepository;        
    }

    public override async Task<GetCarResult> ExecuteAsync(GetCarCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.CarId.HasValue)
        {
            var car = await _carRepository.GetAsync(command.CarId.Value, cancellationToken);

            if (car is null) throw new Exception($"Car with id {command.CarId.Value} not found.");

            var carDto = new Dtos.CarDto(car.Id, car.Brand, car.Model, car.Year, car.Price);


            return new GetCarResult(carDto);
        }

        var cars = await _carRepository.GetAllAsync(cancellationToken);

        var carsDto = cars.Select(car =>        
            new Dtos.CarDto(car.Id, car.Brand, car.Model, car.Year, car.Price)).ToList();

        return new GetCarResult(carsDto);
    }
}