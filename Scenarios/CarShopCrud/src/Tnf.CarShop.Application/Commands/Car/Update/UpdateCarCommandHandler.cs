using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Commands.Car.Update;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Car.Create;
using Tnf.Commands;

namespace Tnf.CarShop.Host.Commands.Car.Update;

public class UpdateCarCommandHandler : ICommandHandler<UpdateCarCommand, UpdateCarResult>
{
    private readonly ILogger<UpdateCarCommandHandler> _logger;
    private readonly ICarRepository _carRepository;
    private readonly IDealerRepository _dealerRepository;
    private readonly ICustomerRepository _customerRepository;

    public UpdateCarCommandHandler(ILogger<UpdateCarCommandHandler> logger, ICarRepository carRepository, IDealerRepository dealerRepository, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
        _dealerRepository = dealerRepository;
        _customerRepository = customerRepository;
    }

    public async Task HandleAsync(ICommandContext<UpdateCarCommand, UpdateCarResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var carDto = context.Command.Car;

        var car = await _carRepository.GetAsync(carDto.Id, cancellationToken);
        
        if (car == null)
        {
            throw new Exception($"Car with id {carDto.Id} not found.");
        }

        car.UpdateBrand(carDto.Brand);
        car.UpdateModel(carDto.Model);
        car.UpdatePrice(carDto.Price);
        car.UpdateYear(carDto.Year);

        if (carDto.DealerId.HasValue)
        {
            var dealer = await _dealerRepository.GetAsync(carDto.DealerId.Value, cancellationToken);
            car.AssignToDealer(dealer);
        }
        
        if (carDto.OwnerId.HasValue)
        {
            var owner = await _customerRepository.GetAsync(carDto.OwnerId.Value, cancellationToken);
            car.AssignToOwner(owner);
        }
        
        var updatedCar = await _carRepository.UpdateAsync(car, cancellationToken);

        context.Result = new UpdateCarResult(new CarDto(updatedCar.Id, updatedCar.Brand, updatedCar.Model, updatedCar.Year, updatedCar.Price, updatedCar.Dealer?.Id, updatedCar.Owner?.Id) );

        return;
    }
    
}