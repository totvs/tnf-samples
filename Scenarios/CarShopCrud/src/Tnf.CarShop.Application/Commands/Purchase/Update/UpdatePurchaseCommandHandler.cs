using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Purchase;
using Tnf.Commands;

public class UpdatePurchaseCommandHandler : ICommandHandler<PurchaseCommand, PurchaseResult>
{
    private readonly ILogger<UpdatePurchaseCommandHandler> _logger;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ICarRepository _carRepository;

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

    public async Task HandleAsync(ICommandContext<PurchaseCommand, PurchaseResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var command = context.Command;

        var purchase = await _purchaseRepository.GetAsync(command.Id, cancellationToken);

        if (purchase == null)
        {
            throw new Exception($"Purchase with id {command.Id} not found.");
        }

        var customer = await _customerRepository.GetAsync(command.CustomerId, cancellationToken);
        if (customer == null)
        {
            throw new Exception($"Customer with id {command.CustomerId} not found.");
        }

        var car = await _carRepository.GetAsync(command.CarId, cancellationToken);
        if (car == null)
        {
            throw new Exception($"Car with id {command.CarId} not found.");
        }

        purchase.UpdateCustomer(customer);
        purchase.UpdateCar(car);

        var updatedPurchase = await _purchaseRepository.UpdateAsync(purchase, cancellationToken);

        context.Result = new PurchaseResult(updatedPurchase.Id, true);

        return;
    }
}