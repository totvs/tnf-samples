using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer.Get;

public class GetCustomerCommandHandler : ICommandHandler<GetCustomerCommand, GetCustomerResult>
{
    private readonly CarFactory _carFactory;
    private readonly CustomerFactory _customerFactory;
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<GetCustomerCommandHandler> _logger;

    public GetCustomerCommandHandler(ILogger<GetCustomerCommandHandler> logger, ICustomerRepository customerRepository,
        CarFactory carFactory, CustomerFactory customerFactory)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _carFactory = carFactory;
        _customerFactory = customerFactory;
    }

    public async Task HandleAsync(ICommandContext<GetCustomerCommand, GetCustomerResult> context,
        CancellationToken cancellationToken = new())
    {
        var command = context.Command;

        if (command.CustomerId.HasValue)
        {
            var customer = await _customerRepository.GetAsync(command.CustomerId.Value, cancellationToken);

            if (customer == null) throw new Exception($"Customer with id {command.CustomerId.Value} not found.");

            var customerDto = _customerFactory.ToDto(customer);

            context.Result = new GetCustomerResult(customerDto);
        }

        var customers = await _customerRepository.GetAllAsync(cancellationToken);

        var customersDto = customers.Select(_customerFactory.ToDto).ToList();

        context.Result = new GetCustomerResult(customersDto);
    }
}