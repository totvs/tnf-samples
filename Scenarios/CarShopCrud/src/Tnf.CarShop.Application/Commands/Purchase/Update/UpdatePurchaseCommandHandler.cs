using Microsoft.Extensions.Logging;

using Tnf.CarShop.Application.Extensions;
using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Repositories;

using Tnf.Commands;
//use xunit
namespace Tnf.CarShop.Application.Commands.Purchase.Update;

public class UpdatePurchaseCommandHandler : CommandHandler<UpdatePurchaseCommand, UpdatePurchaseResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<UpdatePurchaseCommandHandler> _logger;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IStoreRepository _storeRepository;


    public UpdatePurchaseCommandHandler(
        ILogger<UpdatePurchaseCommandHandler> logger,
        IPurchaseRepository purchaseRepository,
        ICustomerRepository customerRepository,
        ICarRepository carRepository,
        IStoreRepository storeRepository)
    {
        _logger = logger;
        _purchaseRepository = purchaseRepository;
        _customerRepository = customerRepository;
        _carRepository = carRepository;
        _storeRepository = storeRepository;
    }

    public override async Task<UpdatePurchaseResult> ExecuteAsync(UpdatePurchaseCommand command, CancellationToken cancellationToken = default)
    {
        var purchase = await _purchaseRepository.GetAsync(command.Id, cancellationToken);

        if (purchase == null)
            throw new Exception($"Purchase with id {command.Id} not found.");

        purchase.UpdatePurchaseDate(command.PurchaseDate);

        await UpdateCustomer(command, purchase, cancellationToken);
        await UpdateCar(command, purchase, cancellationToken);
        await UpdateStore(command, purchase, cancellationToken);

        purchase = await _purchaseRepository.UpdateAsync(purchase, cancellationToken);

        _logger.EntitySuccessfullyUpdated(nameof(purchase), purchase.Id);

        var purchaseDto = BuildPurchaseDto(purchase);

        return new UpdatePurchaseResult(purchaseDto);
    }

    private static PurchaseDto BuildPurchaseDto(Domain.Entities.Purchase purchase)
    {
        var purchaseDto = new PurchaseDto(purchase.Id, purchase.PurchaseDate);
        purchaseDto.Car = purchase.Car.ToDto();
        purchaseDto.Customer = purchase.Customer.ToDto();
        purchaseDto.Store = purchase.Store.ToDto();
        return purchaseDto;
    }

    private async Task UpdateStore(UpdatePurchaseCommand command, Domain.Entities.Purchase purchase, CancellationToken cancellationToken)
    {
        if (command.StoreId.HasValue)
        {
            var store = await _storeRepository.GetAsync(command.StoreId.Value, cancellationToken);
            if (store != null)
                purchase.UpdateStore(store);
        }
    }

    private async Task UpdateCar(UpdatePurchaseCommand command, Domain.Entities.Purchase purchase, CancellationToken cancellationToken)
    {
        if (command.CarId.HasValue)
        {
            var car = await _carRepository.GetAsync(command.CarId.Value, cancellationToken);
            if (car != null)
                purchase.UpdateCar(car);
        }
    }

    private async Task UpdateCustomer(UpdatePurchaseCommand command, Domain.Entities.Purchase purchase, CancellationToken cancellationToken)
    {
        if (command.CustomerId.HasValue)
        {
            var customer = await _customerRepository.GetAsync(command.CustomerId.Value, cancellationToken);
            if (customer != null)
                purchase.UpdateCustomer(customer);
        }
    }
}
