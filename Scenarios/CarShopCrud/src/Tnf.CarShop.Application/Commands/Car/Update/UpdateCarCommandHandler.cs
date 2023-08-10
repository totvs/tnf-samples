using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car.Update;

public class UpdateCarCommandHandler : CommandHandler<UpdateCarCommand, UpdateCarResult>
{
    private readonly CarFactory _carFactory;
    private readonly ICarRepository _carRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IDealerRepository _dealerRepository;
    private readonly ILogger<UpdateCarCommandHandler> _logger;

    public UpdateCarCommandHandler(ILogger<UpdateCarCommandHandler> logger, ICarRepository carRepository,
        IDealerRepository dealerRepository, ICustomerRepository customerRepository, CarFactory carFactory)
    {
        _logger = logger;
        _carRepository = carRepository;
        _dealerRepository = dealerRepository;
        _customerRepository = customerRepository;
        _carFactory = carFactory;
    }

    public override async Task<UpdateCarResult> ExecuteAsync(UpdateCarCommand command, CancellationToken cancellationToken = default)
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

        return new UpdateCarResult(updatedCarDto);
    }    
}