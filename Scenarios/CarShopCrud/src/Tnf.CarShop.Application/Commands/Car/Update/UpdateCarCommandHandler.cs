using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Car.Create;
using Tnf.Commands;

namespace Tnf.CarShop.Host.Commands.Car.Update;

public class UpdateCarCommandHandler : ICommandHandler<CarCommand, CarResult>
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

    public async Task HandleAsync(ICommandContext<CarCommand, CarResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var command = context.Command;

        var car = await _carRepository.GetAsync(command.Id, cancellationToken);
        
        if (car == null)
        {
            throw new Exception($"Car with id {command.Id} not found.");
        }

        car.UpdateBrand(command.Brand);
        car.UpdateModel(command.Model);
        car.UpdatePrice(command.Price);
        car.UpdateYear(command.Year);

        if (command.DealerId.HasValue)
        {
            var dealer = await _dealerRepository.GetAsync(command.DealerId.Value, cancellationToken);
            car.AssignToDealer(dealer);
        }
        
        if (command.OwnerId.HasValue)
        {
            var owner = await _customerRepository.GetAsync(command.OwnerId.Value, cancellationToken);
            car.AssignToOwner(owner);
        }
        
        var updatedCar = await _carRepository.UpdateAsync(car, cancellationToken);

        context.Result = new CarResult(updatedCar.Id, true);

        return;
    }
    
}