using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Purchase.Update;

public class UpdatePurchaseCommandHandler : CommandHandler<UpdatePurchaseCommand, UpdatePurchaseResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<UpdatePurchaseCommandHandler> _logger;
    private readonly IPurchaseRepository _purchaseRepository;


    public UpdatePurchaseCommandHandler(
        ILogger<UpdatePurchaseCommandHandler> logger,
        IPurchaseRepository purchaseRepository,
        ICustomerRepository customerRepository,
        ICarRepository carRepository)
    {
        _logger = logger;
        _purchaseRepository = purchaseRepository;
        _customerRepository = customerRepository;
        _carRepository = carRepository;        
    }

    public override async Task<UpdatePurchaseResult> ExecuteAsync(UpdatePurchaseCommand command,
        CancellationToken cancellationToken = default)
    {      

        var purchase = await _purchaseRepository.GetAsync(command.Id, cancellationToken);

        if (purchase == null) throw new Exception($"Purchase with id {command.Id} not found.");

        var customer = await _customerRepository.GetAsync(command.CustomerId, cancellationToken);
        if (customer == null) throw new Exception($"Customer with id {command.CustomerId} not found.");

        var car = await _carRepository.GetAsync(command.CarId, cancellationToken);
        if (car == null) throw new Exception($"Car with id {command.CarId} not found.");

        purchase.UpdateCustomer(customer);
        purchase.UpdateCar(car);

        var updatedPurchase = await _purchaseRepository.UpdateAsync(purchase, cancellationToken);
        
        var purchaseDto = new PurchaseDto(updatedPurchase.Id, 
                updatedPurchase.PurchaseDate, 
                updatedPurchase.Store.TenantId, 
                updatedPurchase.CarId,
                updatedPurchase.CustomerId,
                updatedPurchase.TenantId);

        return new UpdatePurchaseResult(purchaseDto);
    }
}