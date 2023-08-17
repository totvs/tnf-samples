using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;
using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Car.Update;

public class UpdateCarCommandHandler : CommandHandler<UpdateCarCommand, UpdateCarResult>
{    
    private readonly ICarRepository _carRepository;    
    private readonly ICustomerRepository _customerRepository;    
    private readonly IStoreRepository _dealerRepository;
    private readonly ILogger<UpdateCarCommandHandler> _logger;

    public UpdateCarCommandHandler(ILogger<UpdateCarCommandHandler> logger, ICarRepository carRepository,
        IStoreRepository dealerRepository, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
        _dealerRepository = dealerRepository;
        _customerRepository = customerRepository;
    }

    public override async Task<UpdateCarResult> ExecuteAsync(UpdateCarCommand command,
        CancellationToken cancellationToken = default)
    {
  

        var car = await _carRepository.GetAsync(command.Id, cancellationToken);

        if (car == null) throw new Exception($"Car with id {command.Id} not found.");

        car.UpdateBrand(command.Brand);
        car.UpdateModel(command.Model);
        car.UpdatePrice(command.Price);
        car.UpdateYear(command.Year);       

        var updatedCar = await _carRepository.UpdateAsync(car, cancellationToken);

        var updatedCarDto = new CarDto(updatedCar.Id, updatedCar.Brand, updatedCar.Model, updatedCar.Year, updatedCar.Price);

        return new UpdateCarResult(updatedCarDto);
    }
}