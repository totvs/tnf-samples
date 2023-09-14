using Microsoft.Extensions.Logging;

using Tnf.CarShop.Application.Extensions;
using Tnf.CarShop.Domain.Repositories;

using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Purchase;
public class PurchaseCommandCreateHandler : CommandHandler<PurchaseCommandCreateAdmin, PurchaseResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<PurchaseCommandCreateHandler> _logger;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IStoreRepository _storeRepository;

    public PurchaseCommandCreateHandler(
        ICarRepository carRepository,
        ICustomerRepository customerRepository,
        ILogger<PurchaseCommandCreateHandler> logger,
        IPurchaseRepository purchaseRepository,
        IStoreRepository storeRepository)
    {
        _carRepository = carRepository;
        _customerRepository = customerRepository;
        _logger = logger;
        _purchaseRepository = purchaseRepository;
        _storeRepository = storeRepository;
    }

    public override async Task<PurchaseResult> ExecuteAsync(PurchaseCommandCreateAdmin command, CancellationToken cancellationToken = default)
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


        return new PurchaseResult { PurchaseDto = purchase.ToDto() };
    }   

    
}
