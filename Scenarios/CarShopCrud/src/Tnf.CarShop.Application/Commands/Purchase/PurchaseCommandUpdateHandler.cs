using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Extensions;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Purchase;
public class PurchaseCommandUpdateHandler : CommandHandler<PurchaseCommandUpdate, PurchaseResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<PurchaseCommandUpdateHandler> _logger;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IStoreRepository _storeRepository;

    public PurchaseCommandUpdateHandler(
        ICarRepository carRepository,
        ICustomerRepository customerRepository,
        ILogger<PurchaseCommandUpdateHandler> logger,
        IPurchaseRepository purchaseRepository,
        IStoreRepository storeRepository)
    {
        _carRepository = carRepository;
        _customerRepository = customerRepository;
        _logger = logger;
        _purchaseRepository = purchaseRepository;
        _storeRepository = storeRepository;
    }


    public override async Task<PurchaseResult> ExecuteAsync(PurchaseCommandUpdate command, CancellationToken cancellationToken = default)
    {
        var purchase = await _purchaseRepository.GetAsync(command.Id.Value, cancellationToken) ?? throw new Exception($"Purchase with id {command.Id} not found.");
        purchase.UpdatePurchaseDate(command.PurchaseDate);

        await UpdateCustomer(command, purchase, cancellationToken);
        await UpdateCar(command, purchase, cancellationToken);
        await UpdateStore(command, purchase, cancellationToken);

        purchase = await _purchaseRepository.UpdateAsync(purchase, cancellationToken);

        _logger.EntitySuccessfullyUpdated(nameof(purchase), purchase.Id);

        purchase = await _purchaseRepository.GetAsync(purchase.Id, cancellationToken);

        return new PurchaseResult { PurchaseDto = purchase.ToDto() };
    }

    private async Task UpdateStore(PurchaseCommandUpdate command, Domain.Entities.Purchase purchase, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetAsync(command.StoreId, cancellationToken);
        if (store != null)
            purchase.UpdateStore(store);
    }

    private async Task UpdateCar(PurchaseCommandUpdate command, Domain.Entities.Purchase purchase, CancellationToken cancellationToken)
    {
        var car = await _carRepository.GetAsync(command.CarId, cancellationToken);
        if (car != null)
            purchase.UpdateCar(car);
    }

    private async Task UpdateCustomer(PurchaseCommandUpdate command, Domain.Entities.Purchase purchase, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsync(command.CustomerId, cancellationToken);
        if (customer != null)
            purchase.UpdateCustomer(customer);
    }
}
