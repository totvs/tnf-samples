using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Application.Factories.Interfaces;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car.Get;

public class GetCarCommandHandler : CommandHandler<GetCarCommand, GetCarResult>
{
    private readonly ICarFactory _carFactory;

    private readonly ICarRepository _carRepository;
    private readonly ICustomerFactory _customerFactory;
    private readonly IDealerFactory _dealerFactory;
    private readonly ILogger<GetCarCommandHandler> _logger;

    public GetCarCommandHandler(ILogger<GetCarCommandHandler> logger, ICarRepository carRepository,
        ICarFactory carFactory, IDealerFactory dealerFactory, ICustomerFactory customerFactory)
    {
        _logger = logger;
        _carRepository = carRepository;
        _carFactory = carFactory;
        _dealerFactory = dealerFactory;
        _customerFactory = customerFactory;
    }

    public override async Task<GetCarResult> ExecuteAsync(GetCarCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.CarId.HasValue)
        {
            var car = await _carRepository.GetAsync(command.CarId.Value, cancellationToken);

            if (car is null) throw new Exception($"Car with id {command.CarId.Value} not found.");

            var carDto = _carFactory.ToDto(car);

            carDto.Dealer = car.Dealer != null ? _dealerFactory.ToDto(car.Dealer) : null;
            carDto.Owner = car.Owner != null ? _customerFactory.ToDto(car.Owner) : null;

            return new GetCarResult(carDto);
        }

        var cars = await _carRepository.GetAllAsync(cancellationToken);

        var carsDto = cars.Select(car =>
        {
            var dto = _carFactory.ToDto(car);
            dto.Dealer = car.Dealer != null ? _dealerFactory.ToDto(car.Dealer) : null;
            dto.Owner = car.Owner != null ? _customerFactory.ToDto(car.Owner) : null;
            return dto;
        }).ToList();

        return new GetCarResult(carsDto);
    }
}