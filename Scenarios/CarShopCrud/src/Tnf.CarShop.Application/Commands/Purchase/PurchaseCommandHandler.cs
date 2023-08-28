using Microsoft.Extensions.Logging;

using Tnf.CarShop.Application.Extensions;
using Tnf.CarShop.Domain.Repositories;

using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Purchase;
public class PurchaseCommandHandler : CommandHandler<PurchaseCommand, PurchaseResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<PurchaseCommandHandler> _logger;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IStoreRepository _storeRepository;

    public PurchaseCommandHandler(
        ICarRepository carRepository,
        ICustomerRepository customerRepository,
        ILogger<PurchaseCommandHandler> logger,
        IPurchaseRepository purchaseRepository,
        IStoreRepository storeRepository)
    {
        _carRepository = carRepository;
        _customerRepository = customerRepository;
        _logger = logger;
        _purchaseRepository = purchaseRepository;
        _storeRepository = storeRepository;
    }

    public override async Task<PurchaseResult> ExecuteAsync(PurchaseCommand command, CancellationToken cancellationToken = default)
    {
        Domain.Entities.Purchase purchase;

        if (!command.Id.HasValue)
        {
            purchase = await InsertPurchaseAsync(command, cancellationToken);

            return new PurchaseResult { PurchaseDto = purchase.ToDto() };
        }

        purchase = await UpdatePurchaseAsync(command, cancellationToken);

        return new PurchaseResult { PurchaseDto = purchase.ToDto() };
    }

    private async Task<Domain.Entities.Purchase> InsertPurchaseAsync(PurchaseCommand command, CancellationToken cancellationToken)
    {
        var car = await _carRepository.GetAsync(command.CarId, cancellationToken);
        var customer = await _customerRepository.GetAsync(command.CustomerId, cancellationToken);
        var store = await _storeRepository.GetAsync(command.StoreId, cancellationToken);

        if (car == null)
            throw new Exception("Invalid car.");

        if (customer == null)
            throw new Exception("Invalid customer.");

        if (store == null)
            throw new Exception("Invalid store.");

        var purchase = new Domain.Entities.Purchase(command.CarId, command.CustomerId, command.Price, command.PurchaseDate, command.StoreId);

        purchase = await _purchaseRepository.InsertAsync(purchase, cancellationToken);

        _logger.EntitySuccessfullyCreated(nameof(purchase), purchase.Id);

        purchase = await _purchaseRepository.GetAsync(purchase.Id, cancellationToken);

        return purchase;
    }

    private async Task<Domain.Entities.Purchase> UpdatePurchaseAsync(PurchaseCommand command, CancellationToken cancellationToken)
    {
        var purchase = await _purchaseRepository.GetAsync(command.Id.Value, cancellationToken);

        if (purchase == null)
            throw new Exception($"Purchase with id {command.Id} not found.");

        purchase.UpdatePurchaseDate(command.PurchaseDate);

        await UpdateCustomer(command, purchase, cancellationToken);
        await UpdateCar(command, purchase, cancellationToken);
        await UpdateStore(command, purchase, cancellationToken);

        purchase = await _purchaseRepository.UpdateAsync(purchase, cancellationToken);

        _logger.EntitySuccessfullyUpdated(nameof(purchase), purchase.Id);

        purchase = await _purchaseRepository.GetAsync(purchase.Id, cancellationToken);

        return purchase;
    }

    private async Task UpdateStore(PurchaseCommand command, Domain.Entities.Purchase purchase, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetAsync(command.StoreId, cancellationToken);
        if (store != null)
            purchase.UpdateStore(store);
    }

    private async Task UpdateCar(PurchaseCommand command, Domain.Entities.Purchase purchase, CancellationToken cancellationToken)
    {
        var car = await _carRepository.GetAsync(command.CarId, cancellationToken);
        if (car != null)
            purchase.UpdateCar(car);
    }

    private async Task UpdateCustomer(PurchaseCommand command, Domain.Entities.Purchase purchase, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsync(command.CustomerId, cancellationToken);
        if (customer != null)
            purchase.UpdateCustomer(customer);
    }
}
