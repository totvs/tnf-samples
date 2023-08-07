using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Commands.Purchase.Get;
using Tnf.CarShop.Application.Commands.Purchase.Update;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Purchase;
using Tnf.Commands;

public class UpdatePurchaseCommandHandler : ICommandHandler<UpdatePurchaseCommand, UpdatePurchaseResult>
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

        var returnedCar = await _carRepository.GetAsync(purchaseDto.Car.Id, cancellationToken);
        if (returnedCar == null)
        {
            throw new Exception($"Car with id {purchaseDto.Car.Id} not found.");
        }

        // purchase.UpdateCustomer(customer);
        // purchase.UpdateCar(car);

        var updatedPurchase = await _purchaseRepository.UpdateAsync(purchase, cancellationToken);
        
        var returnedCustomerCars = updatedPurchase.Customer.CarsOwned;
        var customerCars = new List<CarDto>();

        if (returnedCustomerCars != null)
            foreach (var car in returnedCustomerCars)
            {
                var carDto = new CarDto(car.Id, car.Brand, car.Model, car.Year, car.Price, car.Dealer?.Id, car.Owner?.Id);
                customerCars.Add(carDto);
            }
        
        var customerDto =  new CustomerDto(updatedPurchase.Customer.Id, updatedPurchase.Customer.FullName, updatedPurchase.Customer.Address,
            updatedPurchase.Customer.Phone, customerCars, updatedPurchase.Customer.Email, updatedPurchase.Customer.DateOfBirth);

        var returnedCarDto = new CarDto(updatedPurchase.Car.Id, updatedPurchase.Car.Brand, updatedPurchase.Car.Model, updatedPurchase.Car.Year,
            updatedPurchase.Car.Price, updatedPurchase.Car.Dealer?.Id, updatedPurchase.Car.Owner?.Id);
        
        
        context.Result = new UpdatePurchaseResult( new PurchaseDto(purchase.Id, purchase.PurchaseDate, customerDto, returnedCarDto ));
        

        return;
    }
}