using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Application.Factories.Interfaces;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer.Get;

public class GetCustomerCommandHandler : CommandHandler<GetCustomerCommand, GetCustomerResult>
{
    private readonly ICarFactory _carFactory;
    private readonly ICustomerFactory _customerFactory;
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerCommandHandler(ICustomerRepository customerRepository, ICustomerFactory customerFactory,
        ICarFactory carFactory)
    {
        _customerRepository = customerRepository;
        _customerFactory = customerFactory;
        _carFactory = carFactory;
    }

    public override async Task<GetCustomerResult> ExecuteAsync(GetCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.CustomerId.HasValue)
        {
            var customer = await _customerRepository.GetAsync(command.CustomerId.Value, cancellationToken);

            if (customer == null) throw new Exception($"Customer with id {command.CustomerId.Value} not found.");

            var customerDto = _customerFactory.ToDto(customer);

            if (customer.CarsOwned != null && customer.CarsOwned.Any())
                customerDto.Cars = customer.CarsOwned.Select(_carFactory.ToDto).ToList();

            return new GetCustomerResult(customerDto);
        }

        var customers = await _customerRepository.GetAllAsync(cancellationToken);

        var customersDto = customers.Select(customer =>
        {
            var customerDto = _customerFactory.ToDto(customer);

            if (customer.CarsOwned != null && customer.CarsOwned.Any())
                customerDto.Cars = customer.CarsOwned.Select(_carFactory.ToDto).ToList();

            return customerDto;
        }).ToList();

        return new GetCustomerResult(customersDto);
    }
}