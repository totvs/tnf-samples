using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Host.Commands.Car.Create;
public class CreateCarCommandHandler : ICommandHandler<CarCommand, CarResult>
{
    private readonly ILogger<CreateCarCommandHandler> _logger;
    private readonly ICarRepository _carRepository;
    private readonly IDealerRepository _dealerRepository;
    private readonly ICustomerRepository _customerRepository;
    

    public CreateCarCommandHandler(ILogger<CreateCarCommandHandler> logger, ICarRepository carRepository, IDealerRepository dealerRepository, ICustomerRepository customerRepository)
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

        var createdCarId = await CreateCarAsync(command, cancellationToken);


        context.Result = new CarResult(createdCarId, true);

        return;
    }

    private async Task<Guid> CreateCarAsync(CarCommand command, CancellationToken cancellationToken)
    {
        var newCar = new Domain.Entities.Car(command.Brand, command.Model, command.Year, command.Price);

        if (command.DealerId.HasValue)
        {
            var dealer = await FetchDealerAsync(command.DealerId.Value, cancellationToken);
            CheckEntityNotNull(dealer, "Dealer", command.DealerId.Value);
            newCar.AssignToDealer(dealer);
        }
        
        if (command.OwnerId.HasValue)
        {
            var owner = await FetchOwnerAsync(command.OwnerId.Value, cancellationToken);
            CheckEntityNotNull(owner, "Owner", command.OwnerId.Value);
            newCar.AssignToOwner(owner);
        }
        
        var createdCar = await _carRepository.InsertAsync(newCar, cancellationToken);

        return createdCar.Id;
    }
    
    private async Task<Domain.Entities.Dealer> FetchDealerAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dealerRepository.GetAsync(id, cancellationToken);
    }

    private async Task<Domain.Entities.Customer> FetchOwnerAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _customerRepository.GetAsync(id, cancellationToken);
    }

    private void CheckEntityNotNull<T>(T entity, string entityType, Guid id)
    {
        if (entity == null)
        {
            throw new Exception($"{entityType} with id {id} not found.");
        }
    }
    
}