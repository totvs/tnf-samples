using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Commands.Customer.Get;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Customer;
using Tnf.Commands;

public class GetCustomerCommandHandler : ICommandHandler<GetCustomerCommand, GetCustomerResult>
{
    private readonly ILogger<GetCustomerCommandHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerCommandHandler(ILogger<GetCustomerCommandHandler> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task HandleAsync(ICommandContext<GetCustomerCommand, GetCustomerResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var customerId = context.Command.CustomerId;

        var customer = await _customerRepository.GetAsync(customerId, cancellationToken);

        if (customer == null)
        {
            throw new Exception($"Customer with id {customerId} not found.");
        }

        var returnedCars = customer.CarsOwned;

        var cars = new List<CarDto>();

        foreach (var car in returnedCars)
        {
            var carDto = new CarDto(car.Id, car.Brand, car.Model, car.Year, car.Price, car.Dealer.Id, car.Owner.Id);
            cars.Add(carDto);
        }
        
        var customerResult = new GetCustomerResult( new CustomerDto(customer.Id, customer.FullName, customer.Address, customer.Phone, cars, customer.Email, customer.DateOfBirth ));

        context.Result = customerResult;

        return;
    }
}