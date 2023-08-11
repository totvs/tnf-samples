using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Application.Factories.Interfaces;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car.Update;

public class UpdateCarCommandHandler : CommandHandler<UpdateCarCommand, UpdateCarResult>
{
    private readonly ICarFactory _carFactory;
    private readonly ICarRepository _carRepository;
    private readonly ICustomerFactory _customerFactory;
    private readonly ICustomerRepository _customerRepository;
    private readonly IDealerFactory _dealerFactory;
    private readonly IDealerRepository _dealerRepository;
    private readonly ILogger<UpdateCarCommandHandler> _logger;

    public UpdateCarCommandHandler(ILogger<UpdateCarCommandHandler> logger, ICarRepository carRepository,
        IDealerRepository dealerRepository, ICustomerRepository customerRepository, ICarFactory carFactory,
        IDealerFactory dealerFactory, ICustomerFactory customerFactory)
    {
        _logger = logger;
        _carRepository = carRepository;
        _dealerRepository = dealerRepository;
        _customerRepository = customerRepository;
        _carFactory = carFactory;
        _dealerFactory = dealerFactory;
        _customerFactory = customerFactory;
    }

    public override async Task<UpdateCarResult> ExecuteAsync(UpdateCarCommand command,
        CancellationToken cancellationToken = default)
    {
        var carDto = command.Car;

        var car = await _carRepository.GetAsync(carDto.Id, cancellationToken);

        if (car == null) throw new Exception($"Car with id {carDto.Id} not found.");

        car.UpdateBrand(carDto.Brand);
        car.UpdateModel(carDto.Model);
        car.UpdatePrice(carDto.Price);
        car.UpdateYear(carDto.Year);

        if (carDto.Dealer != null)
        {
            var dealer = await _dealerRepository.GetAsync(carDto.Dealer.Id, cancellationToken);
            car.AssignToDealer(dealer);
        }

        if (carDto.Owner != null)
        {
            var owner = await _customerRepository.GetAsync(carDto.Owner.Id, cancellationToken);
            car.AssignToOwner(owner);
        }

        var updatedCar = await _carRepository.UpdateAsync(car, cancellationToken);

        var updatedCarDto = _carFactory.ToDto(updatedCar);

        carDto.Dealer = car.Dealer != null ? _dealerFactory.ToDto(car.Dealer) : null;
        carDto.Owner = car.Owner != null ? _customerFactory.ToDto(car.Owner) : null;

        return new UpdateCarResult(updatedCarDto);
    }
}