using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Purchase.Create;

public class CreatePurchaseCommandHandler : CommandHandler<CreatePurchaseCommand, CreatePurchaseResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CreatePurchaseCommandHandler> _logger;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IStoreRepository _storeRepository;

    public CreatePurchaseCommandHandler(ILogger<CreatePurchaseCommandHandler> logger,
        IPurchaseRepository purchaseRepository, ICarRepository carRepository, ICustomerRepository customerRepository,
        IStoreRepository storeRepository)
    {
        _logger = logger;
        _purchaseRepository = purchaseRepository;
        _carRepository = carRepository;
        _customerRepository = customerRepository;
        _storeRepository = storeRepository;
    }

    public override async Task<CreatePurchaseResult> ExecuteAsync(CreatePurchaseCommand command,
        CancellationToken cancellationToken = default)
    {
        var createdPurchaseId = await CreatePurchaseAsync(command, cancellationToken);

        return new CreatePurchaseResult(createdPurchaseId);
    }

    private async Task<Guid> CreatePurchaseAsync(CreatePurchaseCommand command, CancellationToken cancellationToken)
    {
        var car = await _carRepository.GetAsync(command.CarId, cancellationToken);
        var customer = await _customerRepository.GetAsync(command.CustomerId, cancellationToken);
        var store = await _storeRepository.GetAsync(command.StoreId, cancellationToken);

        if (car == null || customer == null || store == null) throw new Exception("Invalid car or customer.");

        var newPurchase = new Domain.Entities.Purchase(command.CarId, command.CustomerId, command.Price, command.PurchaseDate, command.TenantId);

        var createdPurchase = await _purchaseRepository.InsertAsync(newPurchase, cancellationToken);

        return createdPurchase.Id;
    }
}
