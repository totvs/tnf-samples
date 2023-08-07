using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Commands.Purchase.Get;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

public class GetPurchaseCommandHandler : ICommandHandler<GetPurchaseCommand, GetPurchaseResult>
{
    private readonly ILogger<GetPurchaseCommandHandler> _logger;
    private readonly IPurchaseRepository _purchaseRepository;

    public GetPurchaseCommandHandler(ILogger<GetPurchaseCommandHandler> logger, IPurchaseRepository purchaseRepository)
    {
        _logger = logger;
        _purchaseRepository = purchaseRepository;
    }

    public async Task HandleAsync(ICommandContext<GetPurchaseCommand, GetPurchaseResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var purchaseId = context.Command.PurchaseId;

        var purchase = await _purchaseRepository.GetAsync(purchaseId, cancellationToken);

        if (purchase == null)
        {
            throw new Exception($"Purchase with id {purchaseId} not found.");
        }
        
        var returnedCustomerCars = purchase.Customer.CarsOwned;
        var customerCars = new List<CarDto>();

        if (returnedCustomerCars != null)
            foreach (var car in returnedCustomerCars)
            {
                var carDto = new CarDto(car.Id, car.Brand, car.Model, car.Year, car.Price, car.Dealer?.Id, car.Owner?.Id);
                customerCars.Add(carDto);
            }
        
        var customerDto =  new CustomerDto(purchase.Customer.Id, purchase.Customer.FullName, purchase.Customer.Address,
            purchase.Customer.Phone, customerCars, purchase.Customer.Email, purchase.Customer.DateOfBirth);

        var returnedCarDto = new CarDto(purchase.Car.Id, purchase.Car.Brand, purchase.Car.Model, purchase.Car.Year,
            purchase.Car.Price, purchase.Car.Dealer?.Id, purchase.Car.Owner?.Id);
        
        
        var purchaseResult = new GetPurchaseResult( new PurchaseDto(purchase.Id, purchase.PurchaseDate, customerDto, returnedCarDto ));

        context.Result = purchaseResult;

        return;
    }
}