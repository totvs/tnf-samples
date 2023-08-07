using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Commands.Customer.Get;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Customer;
using Tnf.Commands;

public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, UpdateCustomerResult>
{
    private readonly ILogger<UpdateCustomerCommandHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(ILogger<UpdateCustomerCommandHandler> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task HandleAsync(ICommandContext<UpdateCustomerCommand, UpdateCustomerResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var customerDto = context.Command.Customer;

        var customer = await _customerRepository.GetAsync(customerDto.Id, cancellationToken);

        if (customer == null)
        {
            throw new Exception($"Customer with id {customerDto.Id} not found.");
        }

        customer.UpdateFullName(customerDto.FullName);
        customer.UpdateAddress(customerDto.Address);
        customer.UpdatePhone(customerDto.Phone);
        customer.UpdateEmail(customerDto.Email);
        customer.UpdateDateOfBirth(customerDto.DateOfBirth);

        var updatedCustomer = await _customerRepository.UpdateAsync(customer, cancellationToken);
        
        var returnedCars = updatedCustomer.CarsOwned;
        var cars = new List<CarDto>();

        foreach (var car in returnedCars)
        {
            var carDto = new CarDto(car.Id, car.Brand, car.Model, car.Year, car.Price, car.Dealer.Id, car.Owner.Id);
            cars.Add(carDto);
        }
        
        context.Result = new UpdateCustomerResult( new CustomerDto(updatedCustomer.Id, updatedCustomer.FullName, updatedCustomer.Address, updatedCustomer.Phone, cars, updatedCustomer.Email, updatedCustomer.DateOfBirth));
        
        return;
    }
}