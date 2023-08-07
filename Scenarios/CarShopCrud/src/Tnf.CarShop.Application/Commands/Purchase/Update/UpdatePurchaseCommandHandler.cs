using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Commands.Purchase.Update;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

public class UpdatePurchaseCommandHandler : ICommandHandler<UpdatePurchaseCommand, UpdatePurchaseResult>
{
    private readonly ILogger<UpdatePurchaseCommandHandler> _logger;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ICarRepository _carRepository;
    private readonly PurchaseFactory _purchaseFactory;


    public UpdatePurchaseCommandHandler(
        ILogger<UpdatePurchaseCommandHandler> logger, 
        IPurchaseRepository purchaseRepository,
        ICustomerRepository customerRepository,
        ICarRepository carRepository, PurchaseFactory purchaseFactory)
    {
        _logger = logger;
        _purchaseRepository = purchaseRepository;
        _customerRepository = customerRepository;
        _carRepository = carRepository;
        _purchaseFactory = purchaseFactory;
    }

    public async Task HandleAsync(ICommandContext<UpdatePurchaseCommand, UpdatePurchaseResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var purchaseDto = context.Command.Purchase;

        var purchase = await _purchaseRepository.GetAsync(purchaseDto.Id, cancellationToken);

        if (purchase == null)
        {
            throw new Exception($"Purchase with id {purchaseDto.Id} not found.");
        }

        var customer = await _customerRepository.GetAsync(purchaseDto.Customer.Id, cancellationToken);
        if (customer == null)
        {
            throw new Exception($"Customer with id {purchaseDto.Customer.Id} not found.");
        }

        var car = await _carRepository.GetAsync(purchaseDto.Car.Id, cancellationToken);
        if (car == null)
        {
            throw new Exception($"Car with id {purchaseDto.Car.Id} not found.");
        }

        purchase.UpdateCustomer(customer);
        purchase.UpdateCar(car);

        var updatedPurchase = await _purchaseRepository.UpdateAsync(purchase, cancellationToken);
        
        
        context.Result = new UpdatePurchaseResult( _purchaseFactory.ToDto(updatedPurchase));
        
        return;
    }
}